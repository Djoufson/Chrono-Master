using Application.Features.Courses;
using Application.Features.Courses.Errors;
using Domain.Utilities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Base;
using static Application.Features.Courses.ChangeSessionHour;

namespace WebApi.Controllers;

public class CoursesController : ApiController
{
    private readonly ISender _sender;

    public CoursesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [Authorize(Policy = Policies.AcademicManagerOnly)]
    public async Task<IActionResult> CreateCourse(CreateCourse.CreateCourseRequest request)
    {
        var result = await _sender.Send(request);
        if(result.IsSuccess)
            return Ok(result.Value);

        var error = result.Errors.Select(e => e.Message);
        return result.Errors.First() switch
        {
            DepartmentNotFoundError => BadRequest(error),
            _ => Problem()
        };
    }

    [HttpGet]
    [Authorize(Policy = Policies.AdminAndAcademicManagerOnly)]
    public async Task<IActionResult> GetCourses()
    {
        var query = new GetAllCourses.GetAllCoursesRequest();
        var result = await _sender.Send(query);

        if(result.IsSuccess)
            return Ok(result.Value);

        return Problem();
    }

    [HttpPost("{id:Guid}/definitions")]
    [Authorize(Policy = Policies.AcademicManagerOnly)]
    public async Task<IActionResult> AddDefinition(
        [FromRoute] Guid id,
        AddCourseDefinition.AddCourseDefinitionRequest request)
    {
        var command = new AddCourseDefinition.AddCourseDefinitionCommand(id, request);
        var result = await _sender.Send(command);
        if(result.IsSuccess)
            return Ok(result.Value);

        var error = result.Errors.Select(e => e.Message);
        return result.Errors.First() switch
        {
            ConcurrentScheduleError => BadRequest(error),
            CourseNotFoundError => NotFound(error),
            _ => Problem()
        };
    }

    [HttpPost("{id:Guid}/sessions")]
    [Authorize(Policy = Policies.AcademicManagerOnly)]
    public async Task<IActionResult> ComputeSessions(
        [FromRoute] Guid id,
        ComputeSessionsGeneration.ComputeSessionRequest request
    )
    {
        var command = new ComputeSessionsGeneration.ComputeSessionCommand(id, request.StartDay, request.EndDay, request.Persist);
        var result = await _sender.Send(command);

        if(result.IsSuccess)
            return Ok(result.Value);

        var error = result.Errors.Select(e => e.Message);
        return result.Errors.First() switch
        {
            CourseNotFoundError => NotFound(error),
            _ => Problem()
        };
    }

    [HttpPut("{id:Guid}/sessions/{sessionId:Guid}")]
    [Authorize(Policy = Policies.AcademicManagerOnly)]
    public async Task<IActionResult> ChangeSessionHour(
        [FromRoute] Guid id,
        [FromRoute] Guid sessionId,
        ChangeSessionHourRequest request
    )
    {
        var command = new ChangeSessionHourCommand(id, sessionId, request);
        var result = await _sender.Send(command);

        if(result.IsSuccess)
            return Ok(result.Value);

        var error = result.Errors.Select(e => e.Message);
        return result.Errors.First() switch
        {
            CourseNotFoundError or SessionNotFoundError => NotFound(error),
            PlanningNotSetError => BadRequest(error),
            _ => Problem()
        };
    }
}

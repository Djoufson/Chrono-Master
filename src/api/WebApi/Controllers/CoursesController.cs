using Application.Features.Courses;
using Application.Features.Courses.Errors;
using Domain.Utilities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Base;

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
        else
            return Problem();
    }
}

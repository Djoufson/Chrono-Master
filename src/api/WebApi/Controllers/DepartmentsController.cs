using Application.Features.Departments;
using Application.Features.Departments.Errors;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Base;

namespace WebApi.Controllers;

public class DepartmentsController : ApiController
{
    private readonly ISender _sender;

    public DepartmentsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreateDepartment(CreateDepartment.CreateDepartmentRequest request)
    {
        var result = await _sender.Send(request);
        if(result.IsSuccess)
            return Ok(result.Value);

        var error = result.Errors.Select(e => e.Message);
        return result.Errors.First() switch
        {
            DepartmentConflictError => Conflict(error),
            _ => Problem()
        };
    }
}

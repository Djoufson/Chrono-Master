using Application.Features.Authentication;
using Application.Features.Authentication.Errors;
using Domain.Utilities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api")]
public class AuthController : ControllerBase
{
    private readonly ISender _sender;

    public AuthController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(Login.LoginRequest request)
    {
        var result = await _sender.Send(request);
        if(result.IsSuccess)
            return Ok(result.Value);

        var error = result.Errors.Select(e => e.Message);
        return result.Errors.First() switch
        {
            BadCredentialsError or PasswordRequirementsError => BadRequest(error),
            UserNotFoundError => NotFound(error),
            _ => Problem()
        };
    }

    [HttpPost("register")]
    [Authorize(Policy = Policies.AdminOnly)]
    public async Task<IActionResult> Register(Register.RegisterRequest request)
    {
        var result = await _sender.Send(request);
        if(result.IsSuccess)
            return Ok(result.Value);

        var error = result.Errors.Select(e => e.Message);
        return BadRequest(error);
    }
}

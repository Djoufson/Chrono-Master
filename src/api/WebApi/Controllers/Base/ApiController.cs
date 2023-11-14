using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Base;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiController : ControllerBase
{
}

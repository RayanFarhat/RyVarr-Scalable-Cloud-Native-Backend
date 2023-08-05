using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BackendServer.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace BackendServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserDataController : ControllerBase
{

    private readonly ILogger<UserDataController> _logger;

    public UserDataController(ILogger<UserDataController> logger)
    {
        _logger = logger;
    }

    // [Authorize(Roles = UserRoles.User)]
    [Authorize]
    [HttpGet]
    public IActionResult Get()
    {
        string? userName = User.FindFirst(ClaimTypes.Name)?.Value;
        return Ok(new Response { Status = "Success", Message = $"your username is  {userName}!!!" });
    }
}

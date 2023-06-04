using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers;


[Route("api/[controller]")]
[ApiController]
public class HelloWorldController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Hello, World!");
    }
}

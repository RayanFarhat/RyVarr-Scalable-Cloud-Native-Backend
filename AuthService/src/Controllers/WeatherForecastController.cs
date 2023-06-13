using Microsoft.AspNetCore.Mvc;
using AuthService.DB;

namespace AuthService.Controllers;


[Route("api/[controller]")]
[ApiController]
public class HelloWorldController : ControllerBase
{
    private readonly UserContext _dbContext;
    public HelloWorldController(UserContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Hello, World!");
    }
}

using Microsoft.AspNetCore.Mvc;
using AuthService.DB;
using StackExchange.Redis;

namespace AuthService.Controllers;


[Route("[controller]")]
[ApiController]
public class HelloWorldController : ControllerBase
{
    private readonly UserContext _dbContext;
    private readonly IConnectionMultiplexer _redis;

    public HelloWorldController(UserContext dbContext, IConnectionMultiplexer redis)
    {
        _dbContext = dbContext;
        _redis = redis;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Hello, World!");
    }

    [HttpGet("foo")]
    public async Task<IActionResult> Foo()
    {
        var db = _redis.GetDatabase();
        string key = "k";
        string value = "some value";

        await db.StringSetAsync(key, value);
        var foo = await db.StringGetAsync(key);
        return Ok(foo.ToString());
    }
}

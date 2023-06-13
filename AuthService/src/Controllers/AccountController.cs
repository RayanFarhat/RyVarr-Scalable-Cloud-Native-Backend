using Microsoft.AspNetCore.Mvc;
using AuthService.DB;
using StackExchange.Redis;

namespace AuthService.Controllers;


[Route("auth/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly RyvarrDb _dbContext;
    private readonly IConnectionMultiplexer _redis;

    public AccountController(RyvarrDb dbContext, IConnectionMultiplexer redis)
    {
        _dbContext = dbContext;
        _redis = redis;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Hello, World!");
    }

    [HttpPost]
    public IActionResult Post()
    {
        return Ok("Post Hello, World!");
    }

    [HttpPatch]
    public IActionResult Patch()
    {
        return Ok("Patch Hello, World!");
    }

    [HttpDelete]
    public IActionResult Delete()
    {
        return Ok("Delete Hello, World!");
    }

    // test for redis
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

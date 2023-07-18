using Microsoft.AspNetCore.Mvc;
using AuthService.DB;
using StackExchange.Redis;
using AuthService.DTOs;
using System;


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

    [HttpGet("{sessionID}")]
    public async Task<IActionResult> Get(string sessionID)
    {
        var db = _redis.GetDatabase();
        var userID = await db.StringGetAsync(sessionID);
        if (userID.HasValue)
        {
            return Ok(userID.ToString());
        }
        else
            return Ok("not value found for this id");
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] UserForRegister user)
    {
        if (_dbContext.GetByUsername(user.username) != null)
        {
            await _dbContext.Add(user);

            string key = Guid.NewGuid().ToString("N");
            var userAfterInserting = await _dbContext.GetByUsername(user.username);
            TimeSpan expiration = TimeSpan.FromDays(7);
            await _redis.GetDatabase().StringSetAsync(key, userAfterInserting.id.ToString(), expiration);

            return Ok(key);
        }
        else
        {
            return Ok("user is exist, use other username");
        }
    }

    [HttpPatch]
    public async Task<IActionResult> Patch([FromBody] User user)
    {
        await _dbContext.Update(user);
        return Ok("Patch Hello, World!");
    }

    [HttpDelete("{sessionID}")]
    public async Task<IActionResult> Delete(string sessionID)
    {
        var db = _redis.GetDatabase();
        var userID = await db.StringGetAsync(sessionID);
        if (userID.HasValue)
        {
            await _dbContext.Delete(userID.ToString());
            return Ok("Delete Done");
        }
        else
            return Ok("not value found for this id");
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

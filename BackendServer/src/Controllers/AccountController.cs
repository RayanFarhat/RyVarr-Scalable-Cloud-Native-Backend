// using Microsoft.AspNetCore.Mvc;
// using BackendServer.DB;
// using BackendServer.DTOs;
// using System;


// namespace BackendServer.Controllers;


// [Route("auth/[controller]")]
// [ApiController]
// public class AccountController : ControllerBase
// {
//     private readonly RyvarrDb _dbContext;
//     public AccountController(RyvarrDb dbContext)
//     {
//         _dbContext = dbContext;
//     }

//     [HttpGet("{sessionID}")]
//     public async Task<IActionResult> Get(string sessionID)
//     {
//         return Ok("not value found for this id");
//     }

//     [HttpPost]
//     public async Task<IActionResult> Post([FromBody] UserForRegister user)
//     {
//         return Ok("user is exist, use other username");

//     }

//     [HttpPatch]
//     public async Task<IActionResult> Patch([FromBody] User user)
//     {
//         await _dbContext.Update(user);
//         return Ok("Patch Hello, World!");
//     }

//     [HttpDelete("{sessionID}")]
//     public async Task<IActionResult> Delete(string sessionID)
//     {
//         return Ok("not value found for this id");
//     }
// }
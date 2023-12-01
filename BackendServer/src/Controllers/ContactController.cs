using Microsoft.AspNetCore.Mvc;
using BackendServer.Authentication;
using BackendServer.DB;
namespace BackendServer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactController : ControllerBase
{
    private readonly RyvarrDb _db;
    public ContactController(RyvarrDb db)
    {
        _db = db;
    }
    [HttpPost]
    public async Task<IActionResult> post([FromBody] ContactModel model)
    {
        try
        {
            await _db.AddContactData(model);
        }
        catch (System.Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = e.Message.ToString() });
        }
        return Ok(new Response { Status = "Success", Message = "Message recived" });
    }
}
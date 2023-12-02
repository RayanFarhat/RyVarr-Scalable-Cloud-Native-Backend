using Microsoft.AspNetCore.Mvc;
using BackendServer.Authentication;
using BackendServer.DB;
namespace BackendServer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactController : ControllerBase
{
    private int _counter = 0;
    private DateTime _date = DateTime.Now;
    private readonly RyvarrDb _db;
    public ContactController(RyvarrDb db)
    {
        _db = db;
    }
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ContactModel model)
    {
        try
        {
            TimeSpan difference = DateTime.Now - _date;

            if (difference.TotalDays > 1)
            {
                _counter = 1;
                _date = DateTime.Now;
                await _db.AddContactData(model);
                return Ok(new Response { Status = "Success", Message = "Message recived" });
            }
            else
            {
                if (_counter < 10)
                {
                    _counter++;
                    await _db.AddContactData(model);
                    return Ok(new Response { Status = "Success", Message = "Message recived" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable,
                     new Response { Status = "Error", Message = "max requests for today" });
                }

            }
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
             new Response { Status = "Error", Message = e.Message.ToString() });
        }
    }
}
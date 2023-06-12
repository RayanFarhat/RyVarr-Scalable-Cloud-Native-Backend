using Microsoft.AspNetCore.Mvc;

namespace ChessService.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController : ControllerBase
{

    [HttpPost]
    [Route("")]
    public string Start()
    {
        return "aa";
    }
}
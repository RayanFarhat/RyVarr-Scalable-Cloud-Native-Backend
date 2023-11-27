using Microsoft.AspNetCore.Mvc;
using BackendServer.Services;
using BackendServer.DB;
using BackendServer.DistributedGrains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace BackendServer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HamzaCADController : ControllerBase
{
    private readonly IFileManager _iFileManager;
    private readonly AccountDataCache accountDataCache;

    public HamzaCADController(IFileManager iFileManager
    , RyvarrDb db, IClusterClient clusterClient, UserManager<IdentityUser> userManager)
    {
        _iFileManager = iFileManager;
        accountDataCache = new AccountDataCache(clusterClient, db, userManager);
    }

    [Authorize]
    [HttpGet]
    [Route("downloadfile")]
    public async Task<IActionResult> DownloadFile()
    {
        //get user and check if he is pro
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        var row = await accountDataCache.Get(userId);
        if (row == null)
            return NotFound("Cannot find your user data");

        if (row.IsPro == false)
        {
            return Unauthorized("you are not pro");
        }
        //if user is pro
        else
        {
            var result = await _iFileManager.DownloadFile("aa.txt");
            return File(result.Item1, result.Item2, result.Item3);
        }
    }
}
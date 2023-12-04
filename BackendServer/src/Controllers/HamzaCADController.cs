using Microsoft.AspNetCore.Mvc;
using BackendServer.Services;
using BackendServer.Authentication;
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
    [HttpPost]
    [Route("angle")]
    public IActionResult GetAngle([FromBody] AngleModel model)
    {
        string expireDateString = User.FindFirst(ClaimTypes.DateOfBirth)?.Value!;
        DateTime expireDate = DateTime.ParseExact(expireDateString, "dd-MM-yyyy hh:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

        if (expireDate < DateTime.Now)
            // date has passed.
            return Unauthorized("Your token has been expired !!!");
        else
            // the date is in the future
            // then get the data
            return Ok(GetRotationAngleToXOrY(model.p1x, model.p1y, model.p2x, model.p2y));
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
            var result = await _iFileManager.DownloadFile("HamzaCAD2024Setup.msi");
            return File(result.Item1, result.Item2, result.Item3);
        }
    }


    // check how many to rotate so the polgon lines are parallel to X and Y
    private static double GetRotationAngleToXOrY(double p1x, double p1y, double p2x, double p2y)
    {
        // Calculate the vector from p1 to p2
        double deltaX = p2x - p1x;
        double deltaY = p2y - p1y;

        // Calculate the angle degree between the vector and the X-axis
        double angleToXAxis = Math.Atan2(deltaY, deltaX) * (180.0 / Math.PI);

        // Calculate the rotation angle to make the line parallel to the X-axis (or Y-axis)
        double rotationAngle = 90.0 - angleToXAxis;

        return rotationAngle;
    }
}
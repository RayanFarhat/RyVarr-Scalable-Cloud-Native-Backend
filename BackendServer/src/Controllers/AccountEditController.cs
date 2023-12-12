using BackendServer.Authentication;
using BackendServer.DB;
using BackendServer.DistributedGrains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace BackendServer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountEditController : ControllerBase
{
    private readonly UserManager<IdentityUser> userManager;
    private readonly IConfiguration _configuration;
    public AccountEditController(UserManager<IdentityUser> userManager, IConfiguration configuration,
     RyvarrDb db, IClusterClient clusterClient)
    {
        this.userManager = userManager;
        _configuration = configuration;
    }

    [Authorize]
    [HttpPost]
    [Route("SendEmailConfirmation")]
    public async Task<IActionResult> SendEmailConfirmation()
    {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "Can't find the user" });
        }
        else
        {
            //add token to verify the email
            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var conformedLink = Url.Action(nameof(AccountController.ConformEmail), "Account", new { token, email = user.Email });
            var appPassword = Environment.GetEnvironmentVariable("APP_PASSWORD");
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("ryvarrofficial@gmail.com", appPassword),
                EnableSsl = true,
                UseDefaultCredentials = false
            };
            if (user.Email == null)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                 new Response { Status = "Error", Message = "Can't find the Email of the user" });
            }
            else
            {
                try
                {
                    client.Send("ryvarrofficial@gmail.com", user.Email, "Conform your email",
                         $"Conform your email address by clicking the link below:\n\n{Environment.GetEnvironmentVariable("BASE_URL")}{conformedLink} \n\n If this is not you then don't click this link");

                    return Ok(new Response { Status = "Success", Message = "Email sent for verification!" });
                }
                catch (System.Exception e)
                {

                    return StatusCode(StatusCodes.Status500InternalServerError,
                                  new Response { Status = "Error", Message = e.Message });
                }

            }
        }
    }
}


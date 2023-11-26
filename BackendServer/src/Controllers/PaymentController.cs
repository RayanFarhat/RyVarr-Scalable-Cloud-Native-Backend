using System.Drawing.Text;
using System.Security.Claims;
using BackendServer.Authentication;
using BackendServer.DB;
using BackendServer.DistributedGrains;
using BackendServer.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayPal.Api;


namespace BackendServer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly APIContext _apiContext;
    private readonly UserManager<IdentityUser> userManager;
    private readonly AccountDataCache accountDataCache;


    public PaymentController(RyvarrDb db, IClusterClient clusterClient, UserManager<IdentityUser> userManager)
    {
        accountDataCache = new AccountDataCache(clusterClient, db, userManager);
        this.userManager = userManager;
        // Set up API context with sandbox credentials
        var clientId = "AQVwEIKS5RtVs9KT8CzUZTyPr9SEmOptrqetrJidhXxWeuY5h2UJNw5gR1QdV1VaPxvzYtTIOdMPRc1T";
        var clientSecret = "EG-X_Ri5AyuI3GEt7Qu3-ZvD7IvUECODBxOZVOzMOWqGkD8MUC7AtXjrKzPPqwS-XfNGQwG6ztURJ5I7";

        var payPalConfig = new Dictionary<string, string>
        {
            { "mode", "sandbox" }
        };
        OAuthTokenCredential tokenCredential = new(clientId, clientSecret, payPalConfig);
        string accessToken = tokenCredential.GetAccessToken();
        _apiContext = new APIContext(accessToken);
    }

    [Authorize]
    [HttpGet]
    [Route("Month")]
    public async Task<IActionResult> PayPalLinkMonth()
    {
        return await PayPalLinkShared("returnMonth", "5.00");
    }

    [Authorize]
    [HttpGet]
    [Route("Year")]
    public async Task<IActionResult> PayPalLinkYear()
    {
        return await PayPalLinkShared("returnYear", "50.00");
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("returnMonth/{userId}")]
    public async Task<string> PayPalReturnMonth([FromRoute] string userId, [FromQuery] string paymentId,
        [FromQuery] string token, [FromQuery] string PayerID)
    {
        return await PayPalReturnShared(userId, paymentId, PayerID, 1);
    }


    [AllowAnonymous]
    [HttpGet]
    [Route("returnYear/{userId}")]
    public async Task<string> PayPalReturnYear([FromRoute] string userId, [FromQuery] string paymentId,
        [FromQuery] string token, [FromQuery] string PayerID)
    {
        return await PayPalReturnShared(userId, paymentId, PayerID, 12);
    }


    [AllowAnonymous]
    [HttpGet]
    [Route("cancel")]
    public string PayPalCancelAsync()
    { return "Your payment has been canceled! please return to RyVarr app"; }


    private async Task AddRoleUserPro(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        await userManager.AddToRoleAsync(user!, UserRoles.UserPro);
        await userManager.RemoveFromRoleAsync(user!, UserRoles.User);
    }


    private async Task<IActionResult> PayPalLinkShared(string ReturnUrl, string Price)
    {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        var row = await accountDataCache.Get(userId);
        if (row == null)
            return NotFound("Cannot find your user data");
        // if (row.IsPro == true)
        //     return BadRequest("you are already a pro user");
        // Create payment details
        var payment = new Payment
        {
            intent = "sale",
            payer = new Payer { payment_method = "paypal" },
            transactions = new List<Transaction>
             {
            new Transaction
            {
                amount = new Amount { currency = "USD", total = Price },
                description = "RyVarr Pro Subscribtion"
            }
             },
            redirect_urls = new RedirectUrls
            {
                return_url = $"http://localhost/api/Payment/{ReturnUrl}/{userId}",
                cancel_url = "http://localhost/api/Payment/cancel"
            }
        };

        // Create payment and get approval URL
        var createdPayment = payment.Create(_apiContext);
        var approvalUrl = createdPayment.links.FirstOrDefault(l => l.rel.Equals("approval_url", StringComparison.OrdinalIgnoreCase))?.href;
        return Ok(new Response { Status = "Success", Message = approvalUrl });
    }

    private async Task<string> PayPalReturnShared(string userId, string paymentId, string PayerID, int months)
    {
        // Use paymentId, token, and payerId to execute payment
        var paymentExecution = new PaymentExecution { payer_id = PayerID };
        var payment = new Payment { id = paymentId };



        // Execute payment to complete the transaction
        var executedPayment = payment.Execute(_apiContext, paymentExecution);

        // Check payment status
        // Payment was successful
        if (executedPayment.state == "approved")
        {
            var row = await accountDataCache.Get(userId);
            if (row == null)
                return "Cannot find your user data";
            // make the user pro and date until sub expired
            if (row.IsPro == false)
            {
                row.IsPro = true;
                row.ProEndingDate = DateTime.Now.AddMonths(months).ToString("dd-MM-yyyy hh:mm:ss");
            }
            //if user already pro
            else
            {
                row.ProEndingDate = DateTime.ParseExact(row.ProEndingDate, "dd-MM-yyyy hh:mm:ss", System.Globalization.CultureInfo.InvariantCulture)
                    .AddMonths(months).ToString("dd-MM-yyyy hh:mm:ss");
            }

            await accountDataCache.AddOrUpdate(row);
            //make his role as userPro
            await AddRoleUserPro(userId);

            return "Your payment has succeeded! please return to RyVarr app";
        }
        // Payment was not successful
        else
        {
            // Handle accordingly
            return "Your payment has failed! please return to RyVarr app";
        }
    }
}
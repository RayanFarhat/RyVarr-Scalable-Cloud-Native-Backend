using System.Security.Claims;
using BackendServer.Authentication;
using BackendServer.DB;
using BackendServer.DistributedGrains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PayPal.Api;


namespace BackendServer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private APIContext _apiContext;
    private readonly UserManager<IdentityUser> userManager;
    private readonly AccountDataCache accountDataCache;


    public PaymentController(RyvarrDb db, IClusterClient clusterClient, UserManager<IdentityUser> userManager)
    {
        accountDataCache = new AccountDataCache(clusterClient, db, userManager);
        this.userManager = userManager;
        // Set up API context with credentials
        var clientId = Environment.GetEnvironmentVariable("PAYPAL_CLIENT_ID");
        var clientSecret = Environment.GetEnvironmentVariable("PAYPAL_CLIENT_SECRET");

        var payPalConfig = new Dictionary<string, string>
        {
            { "mode", Environment.GetEnvironmentVariable("PAYPAL_MODE")! }
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
        return await PayPalLinkShared("returnMonth", "9.99");
    }

    [Authorize]
    [HttpGet]
    [Route("Year")]
    public async Task<IActionResult> PayPalLinkYear()
    {
        return await PayPalLinkShared("returnYear", "99.99");
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("returnMonth/{userId}")]
    public async Task<IActionResult> PayPalReturnMonth([FromRoute] string userId, [FromQuery] string paymentId,
        [FromQuery] string token, [FromQuery] string PayerID)
    {
        return await PayPalReturnShared(userId, paymentId, PayerID, 1);
    }


    [AllowAnonymous]
    [HttpGet]
    [Route("returnYear/{userId}")]
    public async Task<IActionResult> PayPalReturnYear([FromRoute] string userId, [FromQuery] string paymentId,
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
            return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "Can't find the user" });
        // if (row.IsPro == true)
        //     return BadRequest("you are already a pro user");
        // Create payment details
        var payment = new Payment
        {
            intent = "sale",
            payer = new Payer { payment_method = "paypal" },
            transactions = new List<Transaction>
             {
            new() {
                amount = new Amount { currency = "USD", total = Price },
                description = "RyVarr Pro Subscribtion"
            }
             },
            redirect_urls = new RedirectUrls
            {
                return_url = Environment.GetEnvironmentVariable("BASE_URL") + $"/api/Payment/{ReturnUrl}/{userId}",
                cancel_url = Environment.GetEnvironmentVariable("BASE_URL") + "/api/Payment/cancel"
            }
        };

        // Set up API context with credentials
        var clientId = Environment.GetEnvironmentVariable("PAYPAL_CLIENT_ID");
        var clientSecret = Environment.GetEnvironmentVariable("PAYPAL_CLIENT_SECRET");

        var payPalConfig = new Dictionary<string, string>
        {
            { "mode", Environment.GetEnvironmentVariable("PAYPAL_MODE")! }
        };
        OAuthTokenCredential tokenCredential = new(clientId, clientSecret, payPalConfig);
        string accessToken = tokenCredential.GetAccessToken();
        _apiContext = new APIContext(accessToken);

        // Create payment and get approval URL
        var createdPayment = payment.Create(_apiContext);
        var approvalUrl = createdPayment.links.FirstOrDefault(l => l.rel.Equals("approval_url", StringComparison.OrdinalIgnoreCase))?.href;
        if (approvalUrl != null)
        {
            return Ok(new Response { Status = "Success", Message = approvalUrl });
        }
        else
        {
            return Ok(new Response { Status = "Failed", Message = "error while getting the link" });
        }
    }

    private async Task<IActionResult> PayPalReturnShared(string userId, string paymentId, string PayerID, int months)
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
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "Can't find the user" });
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

            return Redirect(Environment.GetEnvironmentVariable("BASE_URL") + "/payment/success");

        }
        // Payment was not successful
        else
        {
            // Handle accordingly
            return Redirect(Environment.GetEnvironmentVariable("BASE_URL") + "/payment/fail");

        }
    }
}
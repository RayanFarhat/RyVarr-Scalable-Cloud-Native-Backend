using System.Drawing.Text;
using System.Security.Claims;
using BackendServer.DB;
using BackendServer.DistributedGrains;
using BackendServer.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayPal.Api;


namespace BackendServer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private APIContext _apiContext;
    private readonly RyvarrDb _db;
    private readonly AccountDataCache accountDataCache;


    public PaymentController(RyvarrDb db, IClusterClient clusterClient)
    {
        accountDataCache = new AccountDataCache(clusterClient, db);
        _db = db;
        // Set up API context with sandbox credentials
        var clientId = "AQVwEIKS5RtVs9KT8CzUZTyPr9SEmOptrqetrJidhXxWeuY5h2UJNw5gR1QdV1VaPxvzYtTIOdMPRc1T";
        var clientSecret = "EG-X_Ri5AyuI3GEt7Qu3-ZvD7IvUECODBxOZVOzMOWqGkD8MUC7AtXjrKzPPqwS-XfNGQwG6ztURJ5I7";

        var payPalConfig = new Dictionary<string, string>();
        payPalConfig.Add("mode", "sandbox");
        OAuthTokenCredential tokenCredential = new(clientId, clientSecret, payPalConfig);
        string accessToken = tokenCredential.GetAccessToken();
        _apiContext = new APIContext(accessToken);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> PayPalLinkAsync()
    {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        var row = await accountDataCache.Get(userId);
        if (row == null)
            return NotFound("Cannot find your user data");
        if (row.IsPro == true)
            return BadRequest("you are already a pro user");
        // Create payment details
        var payment = new Payment
        {
            intent = "sale",
            payer = new Payer { payment_method = "paypal" },
            transactions = new List<Transaction>
             {
            new Transaction
            {
                amount = new Amount { currency = "USD", total = "1.00" },
                description = "RyVarr Pro Subscribtion"
            }
             },
            redirect_urls = new RedirectUrls
            {
                return_url = $"http://localhost/api/Payment/return/{userId}",
                cancel_url = "http://yourwebsite.com/cancel"
            }
        };

        // Create payment and get approval URL
        var createdPayment = payment.Create(_apiContext);
        var approvalUrl = createdPayment.links.FirstOrDefault(l => l.rel.Equals("approval_url", StringComparison.OrdinalIgnoreCase))?.href;
        return Ok(approvalUrl);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("return/{userId}")]
    public async Task<string> PayPalReturnAsync([FromRoute] string userId, [FromQuery] string paymentId,
    [FromQuery] string token, [FromQuery] string PayerID)
    {
        // Use paymentId, token, and payerId to execute payment
        var paymentExecution = new PaymentExecution { payer_id = PayerID };
        var payment = new Payment { id = paymentId };

        var row = await accountDataCache.Get(userId);
        if (row == null)
            return "Cannot find your user data";

        // Execute payment to complete the transaction
        var executedPayment = payment.Execute(_apiContext, paymentExecution);

        // Check payment status
        // Payment was successful
        if (executedPayment.state == "approved")
        {
            // make the user pro
            row.IsPro = true;
            await accountDataCache.AddOrUpdate(row);
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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using PayPal.Api;


namespace BackendServer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{

    [AllowAnonymous]
    [HttpGet]
    public IActionResult PayPalReturn(string paymentId, string token, string PayerID)
    {
        // Use paymentId, token, and payerId to execute payment
        var paymentExecution = new PaymentExecution { payer_id = PayerID };
        var payment = new Payment { id = paymentId };
        // Set up API context with sandbox credentials
        // Set up API context with sandbox credentials
        var clientId = "AQVwEIKS5RtVs9KT8CzUZTyPr9SEmOptrqetrJidhXxWeuY5h2UJNw5gR1QdV1VaPxvzYtTIOdMPRc1T";
        var clientSecret = "EG-X_Ri5AyuI3GEt7Qu3-ZvD7IvUECODBxOZVOzMOWqGkD8MUC7AtXjrKzPPqwS-XfNGQwG6ztURJ5I7";

        var payPalConfig = new Dictionary<string, string>();
        payPalConfig.Add("mode", "sandbox");
        OAuthTokenCredential tokenCredential = new(clientId, clientSecret, payPalConfig);
        string accessToken = tokenCredential.GetAccessToken();
        var apiContext = new APIContext(accessToken);
        // Execute payment to complete the transaction
        var executedPayment = payment.Execute(apiContext, paymentExecution);

        // Check payment status
        if (executedPayment.state == "approved")
        {
            // Payment was successful
            // You can update your backend's records, notify the user, etc.
            System.Console.WriteLine("aaaaaaaaaaaaaa");

        }
        else
        {
            // Payment was not successful
            // Handle accordingly
            System.Console.WriteLine("bbbbbbbbbbb");
        }

        // Redirect user to appropriate page
        return Ok(RedirectToAction("PaymentResult", new { success = executedPayment.state == "approved" }).ToString());
    }
}
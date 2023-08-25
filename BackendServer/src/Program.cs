using BackendServer.Startups;
using PayPal.Api;
var webApplicationOptions = new WebApplicationOptions
{
    ContentRootPath = AppContext.BaseDirectory,
    Args = args,
};
var builder = WebApplication.CreateBuilder(webApplicationOptions);

// Add services to the container.
builder.Services.AddControllers();
await OrleansStartup.Init(builder);
DB_Auth_Startup.Init(builder);
SwaggerStartup.Init(builder);

var app = builder.Build();

SwaggerStartup.InitApp(app);

app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();
app.MapControllers();


// Set up API context with sandbox credentials
// Set up API context with sandbox credentials
var clientId = "AQVwEIKS5RtVs9KT8CzUZTyPr9SEmOptrqetrJidhXxWeuY5h2UJNw5gR1QdV1VaPxvzYtTIOdMPRc1T";
var clientSecret = "EG-X_Ri5AyuI3GEt7Qu3-ZvD7IvUECODBxOZVOzMOWqGkD8MUC7AtXjrKzPPqwS-XfNGQwG6ztURJ5I7";

var payPalConfig = new Dictionary<string, string>();
payPalConfig.Add("mode", "sandbox");
OAuthTokenCredential tokenCredential = new(clientId, clientSecret, payPalConfig);
string accessToken = tokenCredential.GetAccessToken();
var apiContext = new APIContext(accessToken);

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
            description = "RyVarr Subscribtion"
        }
    },
    redirect_urls = new RedirectUrls
    {
        return_url = "http://localhost/api/Payment",
        cancel_url = "http://yourwebsite.com/cancel"
    }
};

// Create payment and get approval URL
var createdPayment = payment.Create(apiContext);
var approvalUrl = createdPayment.links.FirstOrDefault(l => l.rel.Equals("approval_url", StringComparison.OrdinalIgnoreCase))?.href;
System.Console.WriteLine(approvalUrl);

//zeros so he does not have ip and docker assign him one
app.Run("http://0.0.0.0:9090");
//app.Run("http://localhost:9090");
using BackendServer.Startups;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

await OrleansStartup.Init(builder);
DB_Auth_Startup.Init(builder);
SwaggerStartup.Init(builder);

var app = builder.Build();

SwaggerStartup.InitApp(app);
// removed cuz it enforces SSL connections for Redirection
//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//zeros so he does not have ip and docker assign him one
app.Run("http://0.0.0.0:9090");
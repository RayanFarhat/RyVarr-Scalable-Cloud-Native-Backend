using BackendServer.Hubs;
using BackendServer.Startups;
using BackendServer.Services;

var webApplicationOptions = new WebApplicationOptions
{
    ContentRootPath = AppContext.BaseDirectory,
    Args = args,
};
var builder = WebApplication.CreateBuilder(webApplicationOptions);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSignalR();
await OrleansStartup.Init(builder);
DB_Auth_Startup.Init(builder);
SwaggerStartup.Init(builder);

builder.Services.AddTransient<IFileManager, FileManager>();

var app = builder.Build();

SwaggerStartup.InitApp(app);
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHub<HamzaCADHub>("api/hamzacadhub");
//zeros so he does not have ip and docker assign him one
app.Run("http://0.0.0.0:9090");
//app.Run("http://localhost:9090");

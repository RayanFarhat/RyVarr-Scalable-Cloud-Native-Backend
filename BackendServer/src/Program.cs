//using BackendServer.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using BackendServer.Authentication;
using System.Net;
using System.Net.Sockets;
using Orleans.Configuration;
using Orleans.Runtime;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();



var hostName = Dns.GetHostName();
var hostEntry = await Dns.GetHostEntryAsync(hostName);
var ip = hostEntry.AddressList.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);
builder.Host.UseOrleans((_, silo) => silo
.UseRedisClustering("redis:6379")
.AddMemoryGrainStorageAsDefault()
.Configure<EndpointOptions>(options =>
{
    options.AdvertisedIPAddress = ip;
})
);

////////////////////////////
// For Entity Framework  
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql("Host=db;Port=5432;Database=ryvarrdb;Username=ryan;Password=ryan"));
// For Identity  
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

// Adding Authentication  
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
// Adding Jwt Bearer  
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]!))
    };
});

/////////////////////////////

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "localhost", Version = "v1" });

        // Add the Authorization header (Bearer) in Swagger requests
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer"
        });

        // Make sure Swagger UI requires a token to be provided
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new List<string>()
            }
        });
    });

// builder.Services.AddDbContext<RyvarrDb>(options =>
//     options.UseNpgsql("Host=db;Port=5432;Database=ryvarrdb;Username=ryan;Password=ryan"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// removed cuz it enforces SSL connections for Redirection
//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//zeros so he does not have ip and docker assign him one
app.Run("http://0.0.0.0:9090");




// <endpoints>
app.MapGet("/", () => "Hello World!");

app.MapGet("/shorten",
    async (IGrainFactory grains, HttpRequest request, string url) =>
    {
        var host = $"{request.Scheme}://{request.Host.Value}";

        // Validate the URL query string.
        if (string.IsNullOrWhiteSpace(url) &&
            Uri.IsWellFormedUriString(url, UriKind.Absolute) is false)
        {
            return Results.BadRequest($"""
                The URL query string is required and needs to be well formed.
                Consider, ${host}/shorten?url=https://www.microsoft.com.
                """);
        }

        // Create a unique, short ID
        var shortenedRouteSegment = Guid.NewGuid().GetHashCode().ToString("X");

        // Create and persist a grain with the shortened ID and full URL
        var shortenerGrain = grains.GetGrain<IUrlShortenerGrain>(shortenedRouteSegment);
        await shortenerGrain.SetUrl(url);

        // Return the shortened URL for later use
        var resultBuilder = new UriBuilder(host)
        {
            Path = $"/go/{shortenedRouteSegment}"
        };

        return Results.Ok(resultBuilder.Uri);
    });

app.MapGet("/go/{shortenedRouteSegment:required}",
    async (IGrainFactory grains, string shortenedRouteSegment) =>
    {
        // Retrieve the grain using the shortened ID and url to the original URL        
        var shortenerGrain = grains.GetGrain<IUrlShortenerGrain>(shortenedRouteSegment);
        var url = await shortenerGrain.GetUrl();

        return Results.Redirect(url);
    });

app.Run();
// </endpoints>

// <graininterface>
public interface IUrlShortenerGrain : IGrainWithStringKey
{
    Task SetUrl(string fullUrl);

    Task<string> GetUrl();
}
// </graininterface>

// <grain>
public sealed class UrlShortenerGrain : Grain, IUrlShortenerGrain
{
    private readonly IPersistentState<UrlDetails> _state;

    public UrlShortenerGrain(
        [PersistentState(
            stateName: "url",
            storageName: "urls")]
            IPersistentState<UrlDetails> state) => _state = state;

    public async Task SetUrl(string fullUrl)
    {
        _state.State = new()
        {
            ShortenedRouteSegment = this.GetPrimaryKeyString(),
            FullUrl = fullUrl
        };

        await _state.WriteStateAsync();
    }

    public Task<string> GetUrl() =>
        Task.FromResult(_state.State.FullUrl);
}

[GenerateSerializer]
public record class UrlDetails
{
    [Id(0)]
    public string FullUrl { get; set; } = "";

    [Id(1)]
    public string ShortenedRouteSegment { get; set; } = "";
}
// </grain>
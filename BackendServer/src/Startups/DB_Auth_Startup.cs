using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using BackendServer.DB;
using System.Text;

namespace BackendServer.Startups;
public class DB_Auth_Startup
{
    public static void Init(WebApplicationBuilder builder)
    {
        string postgreConnectionstring = "Host=localhost;Port=5432;Database=ryvarrdb;Username=ryan;Password=ryan";
        string? environmentVariableValue = Environment.GetEnvironmentVariable("RUNNING_IN_DOCKER");
        if (environmentVariableValue == "true")
        {
            postgreConnectionstring = "Host=db;Port=5432;Database=ryvarrdb;Username=ryan;Password=ryan";
        }

        // For Entity Framework  
        builder.Services.AddDbContext<RyvarrDb>(options =>
            options.UseNpgsql(postgreConnectionstring));
        // For Identity  
        builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<RyvarrDb>().AddDefaultTokenProviders();

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

    }
}
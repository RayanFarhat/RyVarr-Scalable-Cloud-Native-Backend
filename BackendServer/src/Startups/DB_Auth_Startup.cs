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
        var dbName = Environment.GetEnvironmentVariable("DB_NAME");
        var dbUser = Environment.GetEnvironmentVariable("DB_USER");
        var dbPass = Environment.GetEnvironmentVariable("DB_PASSWORD");
        string postgreConnectionstring = $"Host=localhost;Port=5432;Database={dbName};Username={dbUser};Password={dbPass}";
        string? environmentVariableValue = Environment.GetEnvironmentVariable("RUNNING_IN_DOCKER");
        if (environmentVariableValue == "true")
        {
            postgreConnectionstring = $"Host=db;Port=5432;Database={dbName};Username={dbUser};Password={dbPass}";
        }

        // For Entity Framework  
        builder.Services.AddDbContext<RyvarrDb>(options =>
            options.UseNpgsql(postgreConnectionstring), ServiceLifetime.Singleton);
        // For Identity  
        builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = true;
        }).AddEntityFrameworkStores<RyvarrDb>().AddDefaultTokenProviders();

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

        // we want for our password reset token is to be valid for a limited time
        builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
           opt.TokenLifespan = TimeSpan.FromHours(2));
    }
}
//using BackendServer.DB;
using Microsoft.EntityFrameworkCore;
using BackendServer.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

using BackendServer.Authentication;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

////////////////////////////
// For Entity Framework  
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql("Host=localhost;Port=5432;Database=ryvarrdb;Username=ryan;Password=ryan"));
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


//* important
// Migrate latest database changes during startup
using (var scope = app.Services.CreateScope())
{
    // var dbContext = scope.ServiceProvider
    //     .GetRequiredService<RyvarrDb>();

    // var dbContextaaa = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    // if (dbContextaaa.Database.GetPendingMigrations().Any())
    // {
    //     dbContextaaa.Database.Migrate();
    //     dbContextaaa.SaveChanges();
    // }

    // //Here is the migration executed

    // // *create the tables  based on the context
    // dbContext.Database.Migrate();
    // // Persist changes to the database
    // dbContext.SaveChanges();
}

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




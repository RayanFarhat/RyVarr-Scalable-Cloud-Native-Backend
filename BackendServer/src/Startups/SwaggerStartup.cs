
using Microsoft.OpenApi.Models;
namespace BackendServer.Startups;
public class SwaggerStartup
{
    public static void Init(WebApplicationBuilder builder)
    {
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
    }

    public static void InitApp(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
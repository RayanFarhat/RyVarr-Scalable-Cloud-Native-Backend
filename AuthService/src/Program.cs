using AuthService.DB;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using AuthService.DTOs;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<RyvarrDb>(options =>
    options.UseNpgsql("Host=db;Port=5432;Database=ryvarrdb;Username=ryan;Password=ryan"));

// Add Redis as a service
builder.Services.AddSingleton<IConnectionMultiplexer>(provider =>
{
    var configuration = ConfigurationOptions.Parse("cache");
    return ConnectionMultiplexer.Connect(configuration);
});

var app = builder.Build();

//* important
// Migrate latest database changes during startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider
        .GetRequiredService<RyvarrDb>();

    // Here is the migration executed 
    //* create the tables  based on the context
    dbContext.Database.Migrate();

    dbContext.users.Add(new User(0, "rrr", "eee", "ppp"));
    dbContext.users.Add(new User(0, "rssrr", "essee", "pppss"));
    // Persist changes to the database
    dbContext.SaveChanges();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//zeros so he does not have ip and docker assign him one
app.Run("http://0.0.0.0:9090");




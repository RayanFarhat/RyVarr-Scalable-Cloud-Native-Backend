// * https://prodotnetmemory.com/slides/PerformancePatterns/#1
using ChessService.Chess;
using ChessService;
using ChessService.Hubs;

System.Console.WriteLine(G.gamesManager);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<GameHub>("/GameHub");

// ocelot api with swagger
app.Run("http://0.0.0.0:8080");

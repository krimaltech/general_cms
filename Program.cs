using System.Text.Json.Serialization;
using Backend.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var defaultConnection = Environment.GetEnvironmentVariable("DB_CONNECTION") 
                        ?? builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDbContext<ProjectDbContext>(options =>
{
    options.UseNpgsql(defaultConnection);
});

builder.Services.AddApplicationServices();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapGet("/", () => Results.Ok(new { message = "API is running!" }));

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

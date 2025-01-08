using System.Text.Json.Serialization;
using Backend.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add OpenAPI support
builder.Services.AddOpenApi();

// Get the database connection string
var defaultConnection = Environment.GetEnvironmentVariable("DB_CONNECTION") 
                        ?? builder.Configuration.GetConnectionString("DefaultConnection");

// Register DbContext with Npgsql provider
builder.Services.AddDbContext<ProjectDbContext>(options =>
{
    options.UseNpgsql(defaultConnection);
});

// Add application services
builder.Services.AddApplicationServices();

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// ✅ Automatically apply migrations during startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ProjectDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline
app.MapGet("/", () => Results.Ok(new { message = "API is running!" }));

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(opt => 
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")); 
}); 

builder.Services.AddCors(); 
builder.Services.AddScoped<ITokenService, TokenService>(); //Common Practice: Interface, Implemention Class (Gives high level abstraction, Decoupling)

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod()
    .WithOrigins("http://localhost:4200", "https://localhost:4200")); 

app.MapGet("/", () => {
    try
    {
        return "Server is running";
    }
    catch (Exception ex)
    {
        // Log the exception (e.g., to a file, console, or monitoring system)
        Console.WriteLine($"Error: {ex.Message}");
        return "An unexpected error occurred. Please Check the Console";
    }
});

app.MapControllers();

app.Run();

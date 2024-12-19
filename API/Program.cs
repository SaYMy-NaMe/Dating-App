using System.Text;
using API.Data;
using API.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices(builder.Configuration); 
builder.Services.AddIdentityServices(builder.Configuration);  
var app = builder.Build();

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

// Test database connection and log to the console
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    try
    {
        if (dbContext.Database.CanConnect())
        {
            Console.WriteLine("Database connection successful!");
        }
        else
        {
            Console.WriteLine("Unable to connect to the database.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error checking database connection: {ex.Message}");
    }
}

// Configure the HTTP request pipeline.
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod()
    .WithOrigins("http://localhost:4200", "https://localhost:4200")); 

app.UseAuthentication(); 
app.UseAuthorization(); 

app.MapControllers();

app.Run();

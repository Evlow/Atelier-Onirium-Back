using API.Ioc;
using API.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

// Configure Database connection
builder.Services.ConfigureDBContext(configuration);

// Dependency Injection
builder.Services.ConfigureInjectionDependencyRepository();
builder.Services.ConfigureInjectionDependencyService();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Add Controllers
builder.Services.AddControllers();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("_myAllowSpecificOrigins",
        builder => builder.WithOrigins("http://localhost:3000")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials());
});

// Add Logging
builder.Services.AddLogging(logging =>
{
    logging.ClearProviders(); // Clear default logging providers
    logging.AddConsole(); // Add Console logging (or other logging providers you want)
});

var app = builder.Build();

// Use Middleware for Exception Handling
app.UseMiddleware<ExceptionMiddleware>();

// Enable Swagger in Development mode
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS
app.UseCors("_myAllowSpecificOrigins");

// Use HTTPS redirection
app.UseHttpsRedirection();

// Use Authorization
app.UseAuthorization();

// Map Controllers
app.MapControllers();

// Run the application
app.Run();

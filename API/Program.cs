using API.Data;
using API.Ioc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;
// Configure Database connexion
builder.Services.ConfigureDBContext(configuration);

//Dependency Injection
builder.Services.ConfigureInjectionDependencyRepository();

builder.Services.ConfigureInjectionDependencyService();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseHttpsRedirection();

app.Run();

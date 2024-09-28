using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuration de la chaîne de connexion
var connectionString = builder.Configuration.GetConnectionString("BddConnection");

// Configuration du DbContext et Identity
builder.Services.AddDbContext<AtelierOniriumContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21))
        , mysqlOptions => mysqlOptions.EnableRetryOnFailure())); // Ajoute cette ligne

// Add services to the container.
builder.Services.AddControllers();
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

app.Run();

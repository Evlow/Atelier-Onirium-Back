using API.Ioc;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;
// Configure Database connexion
builder.Services.ConfigureDBContext(configuration);

// Dependency Injection
builder.Services.ConfigureInjectionDependencyRepository();
builder.Services.ConfigureInjectionDependencyService();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("_myAllowSpecificOrigins",
        builder => builder.WithOrigins("http://localhost:3000")  
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Utiliser CORS avant Authorization et HTTPS redirection
app.UseCors("_myAllowSpecificOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

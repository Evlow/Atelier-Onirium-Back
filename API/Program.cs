using System.Text;
using API.Business.Services;
using API.Data;
using API.Entities;
using API.Ioc;
using API.Middleware;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

// Configure Database connection
builder.Services.ConfigureDBContext(configuration);

// Dependency Injection
builder.Services.ConfigureInjectionDependencyRepository();
builder.Services.ConfigureInjectionDependencyService();

// Configure Cloudinary settings

builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));


// Ajoutez le service ImageService
builder.Services.AddSingleton<ImageService>();
builder.Services.AddAutoMapper(typeof(Program));

// Add Controllers
builder.Services.AddControllers();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Jwt auth header",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });

builder.Services.AddIdentityCore<User>(opt=>
{opt.User.RequireUniqueEmail = true;})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<AtelierOniriumContext>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(opt =>
               {
                   opt.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = false,
                       ValidateAudience = false,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                           .GetBytes(builder.Configuration["JWTSettings:TokenKey"]))
                   };
               }); builder.Services.AddAuthorization();
builder.Services.AddScoped<TokenService>();


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
builder.Logging.AddConsole();
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
    app.UseSwaggerUI(
        c => {c.ConfigObject.AdditionalItems.Add("persistAuthorized", "true");}
    );
}

// Enable CORS
app.UseCors("_myAllowSpecificOrigins");

// Use HTTPS redirection
app.UseHttpsRedirection();

app.UseAuthentication();
// Use Authorization
app.UseAuthorization();

// Map Controllers
app.MapControllers();

var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<AtelierOniriumContext>();
var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
// Run the application
app.Run();

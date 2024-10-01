using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuration de la chaîne de connexion
// Récupère la chaîne de connexion à partir du fichier de configuration pour la base de données MySQL
var connectionString = builder.Configuration.GetConnectionString("BddConnection");

// Configuration du DbContext et d'Identity
// Ajoute et configure le contexte de base de données en utilisant MySQL et définit la version du serveur MySQL
// L'option EnableRetryOnFailure permet de réessayer les connexions en cas d'échec, ce qui améliore la résilience
builder.Services.AddDbContext<AtelierOniriumContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21))
        , mysqlOptions => mysqlOptions.EnableRetryOnFailure())); // Ajoute cette ligne pour activer la tolérance aux échecs

// Ajout des services au conteneur d'injection de dépendances
// Ajoute les contrôleurs pour gérer les requêtes HTTP
builder.Services.AddControllers();

// Configure les outils d'exploration des API et la documentation avec Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuration du pipeline de traitement des requêtes HTTP
if (app.Environment.IsDevelopment())
{
    // Active Swagger uniquement en mode développement pour générer une documentation interactive de l'API
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirection des requêtes HTTP vers HTTPS
app.UseHttpsRedirection();

// Utilisation de l'autorisation pour protéger les endpoints si nécessaire
app.UseAuthorization();

// Configure les routes pour les contrôleurs
app.MapControllers();

// Lance l'application
app.Run();

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Ioc
{
    /// <summary>
    /// Classe statique contenant les méthodes d'extension pour configurer les services IoC (Inversion of Control) 
    /// </summary>
    public static class AtelierOniriumIoc
    {
        /// <summary>
        /// Méthode d'extension pour configurer le contexte de la base de données avec Entity Framework Core.
        /// Elle utilise la connexion définie dans le fichier de configuration et intègre MySQL.
        /// </summary>
        /// <param name="services">La collection de services à laquelle ajouter le contexte de base de données.</param>
        /// <param name="configuration">L'objet de configuration qui fournit la chaîne de connexion à la base de données.</param>
        /// <returns>IServiceCollection contenant les services mis à jour avec la configuration du contexte DB.</returns>
        public static IServiceCollection ConfigureDBContext(this IServiceCollection services, IConfiguration configuration)
        {
            // Récupération de la chaîne de connexion depuis le fichier de configuration.
            var connectionString = configuration.GetConnectionString("BddConnection");

            // Configuration du contexte de base de données avec MySQL, détection automatique de la version du serveur,
            // et activation des logs détaillés pour faciliter le débogage.
            services.AddDbContext<IAtelierOniriumDBContext, AtelierOniriumContext>(options => 
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                       .LogTo(Console.WriteLine, LogLevel.Information)  // Journalise les requêtes SQL dans la console.
                       .EnableSensitiveDataLogging()  // Active la journalisation des données sensibles (attention en production).
                       .EnableDetailedErrors());  // Active l'affichage d'erreurs détaillées pour un meilleur débogage.

            return services;
        }
    }
}

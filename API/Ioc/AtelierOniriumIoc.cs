using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using API.Business.Services;
using API.Business.ServicesContract;
using API.Data;
using API.Data.Repository;
using API.Data.RepositoryContract;
using Microsoft.EntityFrameworkCore;

namespace API.Ioc
{


    /// <summary>
    /// Classe statique contenant les méthodes d'extension pour configurer les services IoC (Inversion of Control) 
    /// </summary>
    public static class AtelierOniriumIoc
    {

        // Méthode d'extension pour configurer l'injection de dépendances pour les repositories
        public static IServiceCollection ConfigureInjectionDependencyRepository(this IServiceCollection services)
        {
            // Enregistre ICreationRepository avec une durée de vie Scoped,
            // ce qui signifie qu'une nouvelle instance de CreationRepository est créée pour chaque requête HTTP
            services.AddScoped<ICreationRepository, CreationRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();


            return services;
        }

        // Méthode d'extension pour configurer l'injection de dépendances pour les services
        public static IServiceCollection ConfigureInjectionDependencyService(this IServiceCollection services)
        {
            // Injections des dépendances pour les services métiers
            // - Enregistre ICreationServices avec son implémentation CreationServices
            //   Utilisation d'une durée de vie Scoped, pour que chaque requête HTTP ait une nouvelle instance
            services.AddScoped<ICreationServices, CreationServices>();
            services.AddScoped<IBasketServices, BasketServices>();


            return services;
        }


        /// Méthode d'extension pour configurer le contexte de la base de données avec Entity Framework Core.
        /// Elle utilise la connexion définie dans le fichier de configuration et intègre MySQL.
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

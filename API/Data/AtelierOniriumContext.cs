using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    /// <summary>
    /// Contexte de base de données spécifique à l'application Atelier Onirium. 
    /// Il hérite de DbContext et intègre une interface IAtelierOniriumDBContext pour permettre une injection de dépendance.
    /// </summary>
    public class AtelierOniriumContext : DbContext, IAtelierOniriumDBContext
    {
        /// <summary>
        /// Constructeur par défaut requis pour certaines utilisations, 
        /// comme les tests unitaires ou l'injection de dépendances.
        /// </summary>
        public AtelierOniriumContext()
        { }

        /// <summary>
        /// Constructeur prenant des options de configuration pour le contexte. 
        /// Appelle le constructeur de la classe de base DbContext avec les options fournies.
        /// </summary>
        /// <param name="options">Options spécifiques pour configurer le contexte de base de données, 
        /// telles que la chaîne de connexion et les paramètres du fournisseur de base de données.</param>
        public AtelierOniriumContext(DbContextOptions<AtelierOniriumContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Propriété représentant la table "Creations" dans la base de données. 
        /// Cette DbSet permet d'effectuer des opérations CRUD (Create, Read, Update, Delete) sur les entités de type "Creation".
        /// </summary>
        public DbSet<Creation> Creations { get; set; }
        public DbSet<Basket> Baskets { get; set; }

        
    }
}

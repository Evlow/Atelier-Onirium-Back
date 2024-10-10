using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    /// <summary>
    /// Interface représentant le contrat pour le contexte de base de données de l'application Atelier Onirium.
    /// Elle permet de définir les DbSet d'entités sans exposer directement l'implémentation du contexte.
    /// Cette interface facilite l'injection de dépendances et le remplacement du contexte en fonction des besoins.
    /// </summary>
    public interface IAtelierOniriumDBContext
    {
        /// <summary>
        /// Représente la collection d'entités "Creation" dans la base de données.
        /// Permet d'effectuer des opérations sur les créations stockées dans la table correspondante.
        /// </summary>
        DbSet<Creation> Creations { get; set; }
    }
}

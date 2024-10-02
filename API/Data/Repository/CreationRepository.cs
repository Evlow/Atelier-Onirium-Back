using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.RepositoryContract;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repository
{
    // Implémentation du repository pour accéder aux créations
    public class CreationRepository : ICreationRepository
    {
        // Contexte de base de données injecté pour accéder aux entités
        private readonly IAtelierOniriumDBContext _dBContext;

        // Constructeur qui initialise le contexte de base de données
        public CreationRepository(IAtelierOniriumDBContext dBContext)
        {
            // Correction : Utiliser dBContext au lieu de _dBContext
            _dBContext = dBContext; // Initialisation du contexte de base de données
        }

        // Méthode asynchrone pour récupérer toutes les créations de la base de données
        public async Task<List<Creation>> GetCreationsAsync()
        {
            // Récupération de la liste des créations en utilisant Entity Framework Core
            return await _dBContext.Creations.ToListAsync().ConfigureAwait(false);
        }
       
        public async Task<Creation> GetCreationByIdAsync(int id)
        {

            return await _dBContext.Creations.FirstOrDefaultAsync(creation => creation.Id == id);

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Business.DTO;
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
            public async Task<Creation> GetCreationByNameAsync(string name)
        {

            return await _dBContext.Creations.FirstOrDefaultAsync(creation => creation.Name == name);

        }
        
        public async Task<Creation> CreateCreationAsync(Creation creation)
        {
            var creationAdded = await _dBContext.Creations.AddAsync(creation).ConfigureAwait(false);
            await _dBContext.SaveChangesAsync().ConfigureAwait(false);
            return creationAdded.Entity;
        }

        public async Task<Creation> UpdateCreationAsync (Creation creation)
    {
            var creationUpdated = _dBContext.Creations.Update(creation);

            await _dBContext.SaveChangesAsync().ConfigureAwait(false);

            return creationUpdated.Entity;
        }
           public async Task<Creation> DeleteCreationAsync(Creation creation)
        {
            var creationDeleted = _dBContext.Creations.Remove(creation);
            await _dBContext.SaveChangesAsync().ConfigureAwait(false);
            return creationDeleted.Entity;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Data.RepositoryContract
{
    // Interface pour le repository de créations
    public interface ICreationRepository
    {
        // Méthode asynchrone pour récupérer une liste de créations
        Task<List<Creation>> GetCreationsAsync();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Business.DTO;

namespace API.Business.ServicesContract
{
    // Interface pour définir le contrat du service de gestion des créations
    // Cette interface assure que toutes les implémentations fourniront une méthode pour récupérer les créations
    public interface ICreationServices
    {
        // Méthode asynchrone pour obtenir la liste des créations sous forme de DTOs
        Task<List<CreationDTO>> GetCreationsAsync();
        Task<CreationDTO> GetCreationByIdAsync(int Id);
    }
}

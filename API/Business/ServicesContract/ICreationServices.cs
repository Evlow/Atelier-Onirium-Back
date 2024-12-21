using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Business.DTO;
using API.Entities;

namespace API.Business.ServicesContract
{
    // Interface pour définir le contrat du service de gestion des créations
    // Cette interface assure que toutes les implémentations fourniront une méthode pour récupérer les créations
    public interface ICreationServices
    {
        // Méthode asynchrone pour obtenir la liste des créations sous forme de DTOs
        Task<List<CreationDTO>> GetCreationsAsync();
        Task<CreationDTO> GetCreationByIdAsync(int Id);
        Task<CreationDTO> GetCreationByNameAsync(string name);
        Task<CreationDTO> CreateCreationAsync(CreationDTO creationDTO);
        Task<CreationDTO> UpdateCreationAsync( CreationDTO creationDTO);
        Task<CreationDTO> DeleteCreationAsync(int creationId);
    }
}

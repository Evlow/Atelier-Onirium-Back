using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Business.DTO;
using API.Business.ServicesContract;
using API.Data.RepositoryContract;
using AutoMapper;

namespace API.Business.Services
{
    public class CreationServices : ICreationServices
    {
        // Dépendances injectées pour accéder aux données de création et au mappage d'objets
        private readonly ICreationRepository _creationRepository;
        private readonly IMapper _mapper;

        // Constructeur pour injecter les dépendances nécessaires
        public CreationServices(ICreationRepository creationRepository, IMapper mapper)
        {
            _creationRepository = creationRepository;
            _mapper = mapper;
        }

        // Méthode asynchrone pour obtenir la liste des créations
        // Utilise le repository pour récupérer les créations et les mapper en objets DTO
        public async Task<List<CreationDTO>> GetCreationsAsync()
        {
            // Récupère les créations depuis la base de données de manière asynchrone
            var creations = await _creationRepository.GetCreationsAsync().ConfigureAwait(false);

            // Initialisation d'une liste de DTOs avec la même capacité que la liste des créations récupérées
            List<CreationDTO> listCreationDTO = new List<CreationDTO>(creations.Count);

            // Parcours des créations et mappage vers des objets CreationDTO
            foreach (var creation in creations)
            {
                listCreationDTO.Add(_mapper.Map<CreationDTO>(creation));
            }

            // Retourne la liste des DTOs
            return listCreationDTO;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Business.DTO;
using API.Business.ServicesContract;
using API.Data.RepositoryContract;
using API.Entities;
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

            List<CreationDTO> listCreationDTO = new List<CreationDTO>(creations.Count);

            foreach (var creation in creations)
            {
                listCreationDTO.Add(_mapper.Map<CreationDTO>(creation));
            }

            return listCreationDTO;
        }

        public async Task<CreationDTO> GetCreationByIdAsync(int id)
        {
            var creationGet = await _creationRepository.GetCreationByIdAsync(id).ConfigureAwait(false);
            return _mapper.Map<CreationDTO>(creationGet);

        }


        public async Task<CreationDTO> CreateCreationAsync(CreationDTO creationDTO)
        {
            var isExiste = await CheckCreationNameExisteAsync(creationDTO.Name).ConfigureAwait(false);
            if (isExiste)
                throw new Exception("Il existe déjà une création avec le même nom.");

            var creationToAdd = _mapper.Map<Creation>(creationDTO);

            var creationAdded = await _creationRepository.CreateCreationAsync(creationToAdd).ConfigureAwait(false);

            return _mapper.Map<CreationDTO>(creationAdded);
        }
        public async Task<CreationDTO> UpdateCreationAsync(CreationDTO creation)
        {


            var CreationGet = await _creationRepository.GetCreationByIdAsync(creation.Id).ConfigureAwait(false);
            if (CreationGet == null)
                throw new Exception($"Il n'existe aucune recette avec cet identifiant : {creation.Id}");

            CreationGet.Name = creation.Name;
            CreationGet.Description = creation.Description;
                        CreationGet.PictureUrls = creation.PictureUrls;


            var CreationUpdated = await _creationRepository.UpdateCreationAsync(CreationGet).ConfigureAwait(false);

            return _mapper.Map<CreationDTO>(CreationUpdated);
        }



        public async Task<CreationDTO> DeleteCreationAsync(int creationId)
        {
            var creationGet = await _creationRepository.GetCreationByIdAsync(creationId).ConfigureAwait(false);
            if (creationGet == null)
                throw new Exception($"Il n'existe aucune unité de mesure avec cet identifiant : {creationId}");

            var creationDeleted = await _creationRepository.DeleteCreationAsync(creationGet).ConfigureAwait(false);

            return _mapper.Map<CreationDTO>(creationDeleted);
        }
        private async Task<bool> CheckCreationNameExisteAsync(string creationName)
        {
            var creationGet = await _creationRepository.GetCreationByNameAsync(creationName).ConfigureAwait(false);

            return creationGet != null;
        }
        public async Task<CreationDTO> GetCreationByNameAsync(string name)
        {
            var creationGet = await _creationRepository.GetCreationByNameAsync(name).ConfigureAwait(false);
            return _mapper.Map<CreationDTO>(creationGet);

        }
    }
}

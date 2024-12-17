using API.Business.DTO;
using API.Business.Services;
using API.Business.ServicesContract;
using API.Data;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class CreationController : BaseApiController
    {
        private readonly AtelierOniriumContext _context;
        private readonly ImageService _imageService;
        private readonly ICreationServices _creationService;
        private readonly IMapper _mapper;
        public CreationController(ICreationServices creationService, ImageService imageService, IMapper mapper, AtelierOniriumContext context)
        {
            _mapper = mapper;
            _creationService = creationService;
            _imageService = imageService;
            _context = context;

        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(List<CreationDTO>), 200)]
        public async Task<ActionResult> GetCreationsAsync()
        {
            var creations = await _creationService.GetCreationsAsync().ConfigureAwait(false);

            return Ok(creations);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CreationDTO), 200)]
        public async Task<ActionResult> CreationId(int id)
        {

            var creation = await _creationService.GetCreationByIdAsync(id);

            if (creation == null)
            {
                return NotFound();
            }

            return Ok(creation);

        }
        [HttpPost]
                [Authorize(Roles = "Admin")]

        public async Task<ActionResult> CreateCreationAsync([FromForm] CreationDTO creationDTO)
        {
            var creation = _mapper.Map<CreationDTO>(creationDTO);

            if (creation.File != null)
            {
                var imageResult = await _imageService.AddImageAsync(creation.File);

                if (imageResult.Error != null)
                    return BadRequest(new ProblemDetails { Title = imageResult.Error.Message });


                creation.PictureUrl = imageResult.SecureUrl.ToString();
                creation.PublicId = imageResult.PublicId;

            }

            try
            {
                var creationAdded = await _creationService.CreateCreationAsync(creation).ConfigureAwait(false);
                return Ok(creationAdded);
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    Error = e.Message,
                });
            }
        }


        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateCreationAsync([FromForm] CreationDTO creationDTO)
        {
            // Récupérer la création existante
            var creation = await _creationService.GetCreationByIdAsync(creationDTO.Id).ConfigureAwait(false);

            if (creation == null)
                return NotFound(new ProblemDetails { Title = "La création spécifiée n'existe pas." });

            // Mettre à jour les propriétés via le mapper
            _mapper.Map(creationDTO, creation);
            creation.Name = creationDTO.Name;  
            creation.Description = creationDTO.Description;
            creation.PictureUrl = creationDTO.PictureUrl;   
            creation.PublicId = creationDTO.PublicId;   
            
            // Vérifier s'il y a un fichier image
            if (creationDTO.File != null)
            {
                var imageResult = await _imageService.AddImageAsync(creationDTO.File).ConfigureAwait(false);

                if (imageResult.Error != null)
                    return BadRequest(new ProblemDetails { Title = imageResult.Error.Message });

                // Supprimer l'ancienne image si elle existe
                if (!string.IsNullOrEmpty(creation.PublicId))
                {
                    await _imageService.DeleteImageAsync(creation.PublicId).ConfigureAwait(false);
                }

                // Mettre à jour les détails de l'image
                creation.PictureUrl = imageResult.SecureUrl.ToString();
                creation.PublicId = imageResult.PublicId;
            }

            try
            {
                // Sauvegarder les modifications dans la base de données via le service
                var updatedCreation = await _creationService.UpdateCreationAsync( creation).ConfigureAwait(false);

                // Retourner la création mise à jour
                return Ok(updatedCreation);
            }
            catch (Exception ex)
            {
                // Gestion des exceptions et retour d'une erreur 500
                return StatusCode(500, new ProblemDetails { Title = "Une erreur est survenue lors de la mise à jour.", Detail = ex.Message });
            }
        }



        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteCreationAsync(int id)
        {
            try
            {
                var creationDeleted = await _creationService.DeleteCreationAsync(id).ConfigureAwait(false);

                return Ok(creationDeleted);
                
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    Error = e.Message,
                });
            }



        }

    };
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Business.DTO;
using API.Business.Services;
using API.Business.ServicesContract;
using API.Data;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    public class CreationController : BaseApiController
    {
        private readonly AtelierOniriumContext _context;
        private readonly ImageService _imageService;
        private readonly ICreationServices _creationService;
        private readonly IMapper _mapper;

        // Constructor
        public CreationController(ICreationServices creationService, ImageService imageService, IMapper mapper, AtelierOniriumContext context)
        {
            _mapper = mapper;
            _creationService = creationService;
            _imageService = imageService;
            _context = context;
        }

        // GET: api/Creation
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(List<CreationDTO>), 200)]
        public async Task<ActionResult> GetCreations()
        {
            try
            {
                var creations = await _creationService.GetCreationsAsync();
                return Ok(creations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur est survenue lors de la récupération des créations : {ex.Message}");
            }
        }

        // GET: api/Creation/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCreation(int id)
        {
            try
            {
                var creation = await _creationService.GetCreationByIdAsync(id);
                if (creation == null)
                {
                    return NotFound($"Création avec l'identifiant {id} non trouvée.");
                }
                return Ok(creation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur est survenue lors de la récupération de la création : {ex.Message}");
            }
        }

        // POST: api/Creation
       [HttpPost]
public async Task<ActionResult<CreationDTO>> CreateCreation([FromForm] CreationDTO creationDTO)
{
    try
    {
        // Upload de l'image principale
        if (creationDTO.MainImage != null)
        {
            var mainImageResult = await _imageService.AddImageAsync(creationDTO.MainImage);
            creationDTO.PictureUrl = mainImageResult?.SecureUrl?.ToString();
            creationDTO.PicturePublicId = mainImageResult?.PublicId;
        }

        // Upload des images supplémentaires
        if (creationDTO.AdditionalImages != null && creationDTO.AdditionalImages.Any())
        {
            var additionalImageResults = await _imageService.AddImagesAsync(creationDTO.AdditionalImages);
            creationDTO.PictureUrls = additionalImageResults
                                        .Select(result => result.SecureUrl.ToString())
                                        .ToList();
            creationDTO.PicturePublicIds = additionalImageResults
                                            .Select(result => result.PublicId)
                                            .ToList();
        }

        // Logique pour enregistrer la création dans la base de données (par exemple avec un service de création)
        var creation = await _creationService.CreateCreationAsync(creationDTO);

        return CreatedAtAction(nameof(GetCreation), new { id = creation.Id }, creation);
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Erreur lors de la création : {ex.Message}");
    }
}


        // PUT: api/Creation/{id}
   [HttpPut("{id}")]
public async Task<ActionResult> UpdateCreation(int id, [FromForm] CreationDTO creationDTO, [FromForm] List<IFormFile> files)
{
    try
    {
        if (creationDTO == null)
        {
            return BadRequest("Les données de création sont manquantes.");
        }

        if (creationDTO.Id != id)
        {
            return BadRequest("Les identifiants ne correspondent pas.");
        }

        // Check if there is a new main image to upload and delete the old one if needed
        if (creationDTO.MainImage != null)
        {
            // Delete old main image if it exists
            if (!string.IsNullOrEmpty(creationDTO.PicturePublicId))
            {
                await _imageService.DeleteImageAsync(creationDTO.PicturePublicId);
            }

            // Upload the new main image
            var mainImageResult = await _imageService.AddImageAsync(creationDTO.MainImage);
            creationDTO.PictureUrl = mainImageResult?.SecureUrl?.ToString();
            creationDTO.PicturePublicId = mainImageResult?.PublicId;
        }

        // Check if there are new additional images and delete old ones if needed
        if (creationDTO.AdditionalImages != null && creationDTO.AdditionalImages.Any())
        {
            // Delete old additional images if they exist
            if (creationDTO.PicturePublicIds != null && creationDTO.PicturePublicIds.Any())
            {
                foreach (var publicId in creationDTO.PicturePublicIds)
                {
                    await _imageService.DeleteImageAsync(publicId);
                }
            }

            // Upload new additional images
            var additionalImageResults = await _imageService.AddImagesAsync(creationDTO.AdditionalImages);
            creationDTO.PictureUrls = additionalImageResults
                                        .Select(result => result.SecureUrl.ToString())
                                        .ToList();
            creationDTO.PicturePublicIds = additionalImageResults
                                            .Select(result => result.PublicId)
                                            .ToList();
        }

        var updatedCreation = await _creationService.UpdateCreationAsync(creationDTO);
        return Ok(updatedCreation);
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Une erreur est survenue lors de la mise à jour de la création : {ex.Message}");
    }
}

        // DELETE: api/Creation/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCreation(int id)
        {
            try
            {
                var deletedCreation = await _creationService.DeleteCreationAsync(id);
                return Ok(deletedCreation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur est survenue lors de la suppression de la création : {ex.Message}");
            }
        }
    }
}

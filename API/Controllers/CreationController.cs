using API.Business.DTO;
using API.Business.Services;
using API.Business.ServicesContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class CreationController : BaseApiController
    {
        private readonly ImageService _imageService; private readonly ICreationServices _creationService;
        public CreationController(ICreationServices creationService, ImageService imageService)
        {
            _creationService = creationService;
            _imageService = imageService;
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
        [ProducesResponseType(typeof(CreationDTO), 200)]
        public async Task<ActionResult> CreateCreationAsync([FromForm] CreationDTO creation)
        {
            if (string.IsNullOrWhiteSpace(creation.Name))
            {
                return Problem("Echec : Il manque le nom de la création.");
            }

            if (creation.File != null)
            {
                var imageResult = await _imageService.AddImageAsync(creation.File);

                if (imageResult.Error != null)
                    return BadRequest(new ProblemDetails { Title = imageResult.Error.Message });


                creation.PictureUrl = imageResult.SecureUrl.ToString();
                // creation.Id = imageResult.PublicId;
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


        [HttpPut("{id}")]
        // [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(CreationDTO), 200)]
        public async Task<ActionResult> UpdateCreationAsync(int id, [FromForm] CreationDTO creation)
        {
            if (string.IsNullOrWhiteSpace(creation.Name))
            {
                return Problem("Echec : Il manque le nom de la création.");
            }

            try
            {
                var creationUpdated = await _creationService.UpdateCreationAsync(id, creation).ConfigureAwait(false);

                return Ok(creationUpdated);
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    Error = e.Message,
                });
            }

        }
        [HttpDelete("{id}")]
        // [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(CreationDTO), 200)]
        public async Task<ActionResult> DeleteCreationyAsync(int id)
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


            //  [HttpGet]
            // [AllowAnonymous]
            // [ProducesResponseType(typeof(CreationDTO), 200)]
            // public async Task<ActionResult> CreationByName(string name)
            // {

            //     var creation = await _creationServices.GetCreationByNameAsync(name);

            //     if (creation == null)
            //     {
            //         return NotFound();
            //     }

            //     return Ok(creation);

            // }





        }

    };
}

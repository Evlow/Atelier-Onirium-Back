using API.Business.DTO;
using API.Business.Services;
using API.Business.ServicesContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class CreationController : BaseApiController
    {

        private readonly ICreationServices _creationService;
        public CreationController(ICreationServices creationService)
        {
            _creationService = creationService;
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
        // [Authorize(AuthenticationSchemes = "Bearer")]
        [ProducesResponseType(typeof(CreationDTO), 200)]
        public async Task<ActionResult> CreateCreationAsync([FromBody] CreationDTO creation)
        {
            if (string.IsNullOrWhiteSpace(creation.Name))
            {
                return Problem("Echec : Il manque le nom de la création.");
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







    };
}

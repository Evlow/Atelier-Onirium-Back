using API.Business.DTO;
using API.Business.ServicesContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class CreationController : BaseApiController
    {

        private readonly ICreationServices _creationServices;
        public CreationController(ICreationServices creationServices)
        {
            _creationServices = creationServices;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(List<CreationDTO>), 200)]
        public async Task<ActionResult> GetCreationsAsync()
        {
            var creations = await _creationServices.GetCreationsAsync().ConfigureAwait(false);

            return Ok(creations);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CreationDTO), 200)]
        public async Task<ActionResult> CreationId(int id)
        {

            var creation = await _creationServices.GetCreationByIdAsync(id);

            if (creation == null)
            {
                return NotFound();
            }

            return Ok(creation);

        }

         [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CreationDTO), 200)]
        public async Task<ActionResult> CreationByName(string name)
        {

            var creation = await _creationServices.GetCreationByNameAsync(name);

            if (creation == null)
            {
                return NotFound();
            }

            return Ok(creation);

        }
    };
}

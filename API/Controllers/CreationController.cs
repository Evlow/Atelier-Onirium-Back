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
            try
            {
                var creationId = await _creationServices.GetCreationByIdAsync(id).ConfigureAwait(false);

                return Ok(creationId);
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

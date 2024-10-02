using API.Business.DTO;
using API.Business.ServicesContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CreationController : ControllerBase
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
    }
}

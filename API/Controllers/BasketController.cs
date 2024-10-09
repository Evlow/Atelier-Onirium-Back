using System.Threading.Tasks;
using API.Business.DTO;
using API.Business.ServicesContract;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketServices _basketServices;

        public BasketController(IBasketServices basketServices)
        {
            _basketServices = basketServices;
        }

        // Récupérer le panier via le service
        [HttpGet("{buyerId}", Name = "GetBasket")]
        public async Task<ActionResult<BasketDTO>> GetBasket(string buyerId)
        {
            var basket = await _basketServices.GetBasketAsync(buyerId);
            
            if (basket == null)
                return NotFound(new ProblemDetails { Title = "Basket not found" });

            return Ok(basket);
        }

        // Ajouter un article au panier via le service
        [HttpPost("{buyerId}/items/{productId}/{quantity}")]
        public async Task<ActionResult<BasketDTO>> AddItemToBasket(string buyerId, int productId, int quantity)
        {
            var basket = await _basketServices.AddItemToBasketAsync(buyerId, productId, quantity);

            if (basket == null)
                return BadRequest(new ProblemDetails { Title = "Error adding item to basket" });

            return CreatedAtRoute("GetBasket", new { buyerId = basket.BuyerId }, basket);
        }

        // Supprimer un article du panier via le service
        [HttpDelete("{buyerId}/items/{productId}/{quantity}")]
        public async Task<ActionResult> RemoveItemFromBasket(string buyerId, int productId, int quantity)
        {
            var result = await _basketServices.RemoveItemFromBasketAsync(buyerId, productId, quantity);

            if (!result)
                return BadRequest(new ProblemDetails { Title = "Error removing item from basket" });

            return Ok();
        }

        // Créer un nouveau panier via le service
        [HttpPost("{buyerId}")]
        public async Task<ActionResult<BasketDTO>> CreateBasket(string buyerId)
        {
            var basket = await _basketServices.CreateBasketAsync(buyerId);

            return CreatedAtRoute("GetBasket", new { buyerId = basket.BuyerId }, basket);
        }

        // Supprimer un panier via le service
        [HttpDelete("{buyerId}")]
        public async Task<ActionResult> DeleteBasket(string buyerId)
        {
            var result = await _basketServices.DeleteBasketAsync(buyerId);

            if (!result)
                return BadRequest(new ProblemDetails { Title = "Error deleting basket" });

            return Ok();
        }
    }
}

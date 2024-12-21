// using System;
// using System.Linq;
// using System.Threading.Tasks;
// using API.Business.DTO;
// using API.Data;
// using API.Entities;
// using API.Extensions;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;

// namespace API.Controllers
// {
//     public class BasketController : BaseApiController
//     {
//         private readonly AtelierOniriumContext _context;
//         public BasketController(AtelierOniriumContext context)
//         {
//             _context = context;
//         }

//         [HttpGet(Name = "GetBasket")]
//         public async Task<ActionResult<BasketDTO>> GetBasket()
//         {
//             var basket = await RetrieveBasket(GetBuyerId());

//             if (basket == null) return NotFound();

//             return basket.MapBasketToDto();
//         }

//         [HttpPost("AddItemToBasket/{creationId}/{quantity}")]
//         public async Task<ActionResult<BasketDTO>> AddItemToBasket(int creationId, int quantity)
//         {
//             var basket = await RetrieveBasket(GetBuyerId());

//             if (basket == null) basket = CreateBasket();

//             var creation = await _context.Creations.FindAsync(creationId);

//             if (creation == null) return BadRequest(new ProblemDetails { Title = "creation not found" });

//             basket.AddItem(creation, quantity);

//             var result = await _context.SaveChangesAsync() > 0;

//             if (result) return CreatedAtRoute("GetBasket", basket.MapBasketToDto());

//             return BadRequest(new ProblemDetails { Title = "Problem saving item to basket" });
//         }

//         [HttpDelete("DeleteItem/{creationId}/{quantity}")]
//         public async Task<ActionResult> RemoveBasketItem(int creationId, int quantity)
//         {
//             var basket = await RetrieveBasket(GetBuyerId());

//             if (basket == null) return NotFound();

//             basket.RemoveItem(creationId, quantity);

//             var result = await _context.SaveChangesAsync() > 0;

//             if (result) return Ok();

//             return BadRequest(new ProblemDetails { Title = "Problem removing item from the basket" });
//         }

//         private async Task<Basket> RetrieveBasket(string buyerId)
//         {
//             if (string.IsNullOrEmpty(buyerId))
//             {
//                 Response.Cookies.Delete("buyerId");
//                 return null;
//             }

//             return await _context.Baskets
//                 .Include(i => i.Items)
//                 .ThenInclude(c => c.Creation)
//                 .FirstOrDefaultAsync(x => x.BuyerId == buyerId);
//         }

//         private Basket CreateBasket()
//         {
//             var buyerId = User.Identity?.Name;
//             if (string.IsNullOrEmpty(buyerId))
//             {
//                 buyerId = Guid.NewGuid().ToString();
//                 var cookieOptions = new CookieOptions { IsEssential = true, Expires = DateTime.Now.AddDays(30) };
//                 Response.Cookies.Append("buyerId", buyerId, cookieOptions);
//             }
//             var basket = new Basket { BuyerId = buyerId };
//             _context.Baskets.Add(basket);
//             return basket;
//         }

//         private string GetBuyerId()
//         {
//             return User.Identity?.Name ?? Request.Cookies["buyerId"];
//         }
//     }
// }
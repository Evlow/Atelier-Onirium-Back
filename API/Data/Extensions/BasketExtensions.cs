// using System.Linq;
// using API.Business.DTO;
// using API.Entities;
// using Microsoft.EntityFrameworkCore;

// namespace API.Extensions
// {
//     public static class BasketExtensions
//     {
//         public static BasketDTO MapBasketToDto(this Basket basket)
//         {
//             return new BasketDTO
//             {
//                 Id = basket.Id,
//                 // BuyerId = basket.BuyerId,
//                 // PaymentIntentId = basket.PaymentIntentId,
//                 // ClientSecret = basket.ClientSecret,
//                 Items = basket.Items.Select(item => new BasketItemsDTO
//                 {
//                     CreationId = item.CreationId,
//                     Name = item.Creation.Name,
//                     Price = item.Creation.Price,
//                     PictureUrl = item.Creation.PictureUrl,
//                     Quantity = item.Quantity
//                 }).ToList()
//             };
//         }

//         public static IQueryable<Basket> RetrieveBasketWithItems(this IQueryable<Basket> query, string buyerId)
//         {
//             return query.Include(i => i.Items).ThenInclude(c => c.Creation).Where(b => b.BuyerId == buyerId);
//         }
//     }
// }
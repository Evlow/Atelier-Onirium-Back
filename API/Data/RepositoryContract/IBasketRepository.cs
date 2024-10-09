using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Data.RepositoryContract
{
    public interface IBasketRepository
    {
       Task<Basket> GetBasketAsync(string buyerId);
        Task<Basket> AddItemToBasketAsync(string buyerId, int productId, int quantity);
        Task<bool> RemoveItemFromBasketAsync(string buyerId, int productId, int quantity);
        Task<Basket> CreateBasketAsync(string buyerId);
        Task<bool> DeleteBasketAsync(string buyerId);
        Task<bool> SaveChangesAsync();
    }
}
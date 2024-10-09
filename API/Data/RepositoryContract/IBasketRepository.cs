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
    }
}
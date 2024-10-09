using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Business.DTO;
using API.Entities;

namespace API.Business.ServicesContract
{
    public interface IBasketServices
    {
        Task<BasketDTO> GetBasketAsync(string buyerId);
    }
}
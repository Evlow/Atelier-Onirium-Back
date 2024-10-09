using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.RepositoryContract;
using API.Entities;
using Azure.Core;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repository
{
    public class BasketRepository : IBasketRepository
    {  // Contexte de base de données injecté pour accéder aux entités
        private readonly IAtelierOniriumDBContext _dBContext;

        // Constructeur qui initialise le contexte de base de données
        public BasketRepository(IAtelierOniriumDBContext dBContext)
        {
            // Correction : Utiliser dBContext au lieu de _dBContext
            _dBContext = dBContext; // Initialisation du contexte de base de données
        }
        
         // Récupérer un panier par BuyerId
    public async Task<Basket> GetBasketAsync(string buyerId)
    {
        return await _dBContext.Baskets
            .Include(i => i.Items)
            .ThenInclude(c => c.Creation)
            .FirstOrDefaultAsync(x => x.BuyerId == buyerId);
    }
    }
}
using System;
using System.Threading.Tasks;
using API.Data.RepositoryContract;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IAtelierOniriumDBContext _dBContext;

        public BasketRepository(IAtelierOniriumDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        // Récupérer un panier par BuyerId
        public async Task<Basket> GetBasketAsync(string buyerId)
        {
            return await RetrieveBasketAsync(buyerId);
        }

        public async Task<Basket> AddItemToBasketAsync(string buyerId, int productId, int quantity)
        {
            var basket = await RetrieveBasketAsync(buyerId);

            // Si le panier n'existe pas, créez-en un nouveau
            if (basket == null)
            {
                basket = new Basket { BuyerId = buyerId };
                await _dBContext.Baskets.AddAsync(basket);
            }

            var product = await _dBContext.Creations.FindAsync(productId);
            if (product == null) return null; // Gérer le cas où le produit n'existe pas

            basket.AddItem(product, quantity);

            // Enregistrez les modifications et retournez le panier mis à jour
            await SaveChangesAsync();
            return basket;
        }

        public async Task<Basket> CreateBasketAsync(string buyerId)
        {
            var basket = new Basket { BuyerId = buyerId };
            await _dBContext.Baskets.AddAsync(basket);
            await SaveChangesAsync(); // Enregistrer les changements
            return basket;
        }

        public async Task<bool> RemoveItemFromBasketAsync(string buyerId, int productId, int quantity)
        {
            var basket = await RetrieveBasketAsync(buyerId);
            if (basket == null) return false;

            basket.RemoveItem(productId, quantity);
            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteBasketAsync(string buyerId)
        {
            var basket = await RetrieveBasketAsync(buyerId);
            if (basket == null) return false;

            _dBContext.Baskets.Remove(basket);
            return await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _dBContext.SaveChangesAsync() > 0;
        }

        // Méthode pour récupérer le panier par buyerId
        private async Task<Basket> RetrieveBasketAsync(string buyerId)
        {
            if (string.IsNullOrEmpty(buyerId))
            {
                return null; // Si l'identifiant de l'acheteur est vide, retourne null
            }

            return await _dBContext.Baskets
                .Include(i => i.Items)
                .ThenInclude(c => c.Creation)
                .FirstOrDefaultAsync(x => x.BuyerId == buyerId);
        }
    }
}

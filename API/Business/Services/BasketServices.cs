using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Business.DTO;
using API.Business.ServicesContract;
using API.Data.RepositoryContract;
using API.Entities;
using AutoMapper;

namespace API.Business.Services
{
    public class BasketServices : IBasketServices
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketServices(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        // Méthode pour récupérer le panier via le repository
        public async Task<BasketDTO> GetBasketAsync(string buyerId)
        {
            // Récupération du panier via le repository
            var basketGet = await _basketRepository.GetBasketAsync(buyerId);
            // Mapping de Basket vers BasketDTO avec AutoMapper
            return _mapper.Map<BasketDTO>(basketGet);
        }

        // Méthode pour ajouter un article au panier
        public async Task<BasketDTO> AddItemToBasketAsync(string buyerId, int productId, int quantity)
        {
            var basket = await _basketRepository.AddItemToBasketAsync(buyerId, productId, quantity);
            return _mapper.Map<BasketDTO>(basket);
        }

        // Méthode pour supprimer un article du panier
        public async Task<bool> RemoveItemFromBasketAsync(string buyerId, int productId, int quantity)
        {
            return await _basketRepository.RemoveItemFromBasketAsync(buyerId, productId, quantity);
        }

        // Méthode pour créer un nouveau panier
        public async Task<BasketDTO> CreateBasketAsync(string buyerId)
        {
            var basket = await _basketRepository.CreateBasketAsync(buyerId);
            return _mapper.Map<BasketDTO>(basket);
        }

        // Méthode pour supprimer le panier
        public async Task<bool> DeleteBasketAsync(string buyerId)
        {
            return await _basketRepository.DeleteBasketAsync(buyerId);
        }
    }
}

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
    public class BasketService : IBasketServices
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;


        public BasketService(IBasketRepository basketRepository, IMapper mapper)
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
    }
}
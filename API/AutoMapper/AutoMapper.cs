using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Business.DTO;
using API.Entities;
using AutoMapper;

namespace API.AutoMapper
{
    // Profil AutoMapper personnalisé pour configurer les mappages entre les entités et les DTOs
    public class AutoMapper : Profile
    {
        // Constructeur pour définir les règles de mappage
        public AutoMapper()
        {
            // Mappage bidirectionnel entre l'entité Creation et le DTO CreationDTO
            // ReverseMap() permet de mapper dans les deux sens : de Creation vers CreationDTO et vice-versa
            CreateMap<Creation, CreationDTO>().ReverseMap();
            CreateMap<Basket, BasketDTO>().ReverseMap();
            CreateMap<BasketItems, BasketItemsDTO>().ReverseMap();


        }
    }
}

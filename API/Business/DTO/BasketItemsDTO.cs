using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Business.DTO
{
    public class BasketItemsDTO
    {
        public int Id { get; set; }
        public int CreationId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public long Price { get; set; }
        public string PictureUrl { get; set; }
        public string Category { get; set; }

    }
}
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
        public int Quantity { get; set; }
        public int CreationId { get; set; }

        public Creation Creation { get; set; }

        public int BasketId { get; set; }
        public Basket Basket { get; set; }
    }
}
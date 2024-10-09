using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Business.DTO
{
    public class BasketDTO
    {
                public int Id { get; set; }
        public string BuyerId {get;set;}
                public List<BasketItems> Items {get;set;} = new();



    }
}
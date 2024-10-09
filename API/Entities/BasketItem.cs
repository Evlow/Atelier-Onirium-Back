using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{[Table ("BasketItems")]
    public class BasketItems
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int CreationId { get; set; }

        public Creation Creation { get; set; }

        public int BasketId {get;set;}
        public Basket Basket {get;set;}

    }
}
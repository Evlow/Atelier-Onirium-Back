using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Creation
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public string Category { get; set; }
        public long Price { get; set; }
        public int QuantityInStock { get; set; }


    }
}
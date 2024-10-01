using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{ /// <summary>
  /// Représente une création artistique dans l'application Atelier Onirium.
  /// Cette classe définit les propriétés d'une création, telles que son nom, sa description,
  /// son URL d'image, sa catégorie, son prix et la quantité en stock.
  /// </summary>
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
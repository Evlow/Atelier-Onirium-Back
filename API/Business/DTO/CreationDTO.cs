using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Business.DTO
{
    // DTO (Data Transfer Object) pour représenter une création
    // Utilisé pour transférer les données entre les couches de l'application sans exposer les entités de la base de données directemen
    public class CreationDTO
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
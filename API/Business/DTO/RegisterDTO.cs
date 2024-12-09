using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Business.DTO
{
    public class RegisterDTO : LoginDTO
    {
        [Required(ErrorMessage = "L'adresse email est requis.")]
        public string Email { get; set; }

    }
}
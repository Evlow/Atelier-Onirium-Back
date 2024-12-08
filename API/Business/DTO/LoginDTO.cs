using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Business.DTO
{
    public class LoginDTO
    
{
    [Required(ErrorMessage = "Le nom d'utilisateur est requis.")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Le mot de passe est requis.")]
    public string Password { get; set; }
}
}
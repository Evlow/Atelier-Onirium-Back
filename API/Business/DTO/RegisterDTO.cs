using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Business.DTO
{
    public class RegisterDTO : LoginDTO
    {
        public string Email {get; set;}

    }
}
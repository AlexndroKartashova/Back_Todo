using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Dtos
{
       public class LoginDto
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }

       // public bool Succeeded { get; set; }
    }
}

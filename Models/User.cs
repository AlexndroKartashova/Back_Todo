using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    
    public class User: IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? Birthday { get; set; }
    }
}

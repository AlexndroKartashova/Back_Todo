using Microsoft.AspNetCore.Identity;
using System;

namespace Models
{
    public class Contact
    {
        public int Id { get; set; }

        public String UserId { get; set; }

        public String Name { get; set; }

        public String Email { get; set; }

        public String PhoneNumber { get; set; }

        public Boolean IsDeleted { get; set; }

        public virtual User User { get; set; }

    }
}

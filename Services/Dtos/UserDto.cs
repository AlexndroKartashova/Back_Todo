using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Dtos
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        //public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthday { get; set; }
    }
}

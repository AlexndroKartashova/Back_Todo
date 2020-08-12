using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class UserModel
    {
        public String Id { get; set; }
        public String Email { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public DateTime? Birthday { get; set; }
    }
}

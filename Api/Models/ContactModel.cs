using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class ContactModel
    {
        public int Id { get; set; }

        public String Name { get; set; }

        public String UserId { get; set; }

        public String Email { get; set; }

        public String PhoneNumber { get; set; }

        public Boolean IsDeleted { get; set; }


    }
}

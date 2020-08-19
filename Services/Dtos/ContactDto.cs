using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Dtos
{
    public class ContactDto
    {
        public int Id { get; set; }

        public String UserId { get; set; }

        public String Name { get; set; }

        public String Email { get; set; }

        public String PhoneNumber { get; set; }

        public Boolean IsDeleted { get; set; }

        internal static IEnumerable<ContactDto> OrderBy(Func<object, object> p)
        {
            throw new NotImplementedException();
        }
    }
}

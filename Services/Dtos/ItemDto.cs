using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Dtos
{
    public class ItemDto
    {
        public int Id { get; set; }

        public String Name { get; set; }

        public Boolean IsDone { get; set; }

        public int? ParentItemId { get; set; }
    }
}

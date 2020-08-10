using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class ItemModel
    {
        public int Id { get; set; }

        public String Name { get; set; }

        public Boolean IsDone { get; set; }

        public Boolean IsDeleted { get; set; }

        public int? ParentItemId { get; set; }

        public int ItemCount { get; set; }

        public int DoneItemCount { get; set; }
    }
}

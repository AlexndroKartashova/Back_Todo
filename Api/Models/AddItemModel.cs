using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class AddItemModel
    {
        [Required]
        public String Name { get; set; }

        public int? ParentItemId { get; set; }
    }
}

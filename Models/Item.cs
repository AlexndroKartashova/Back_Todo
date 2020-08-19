using System;

namespace Models
{
    public class Item
    {
        public int Id { get; set; }

        public int? ParentItemId { get; set; }

        public String Name { get; set; }

        public Boolean IsDone { get; set; }

        public String UserId { get; set; }

        public Boolean IsDeleted { get; set; }

        public virtual Item ParentItem { get; set; }

        public virtual User User { get; set; }
    }
}

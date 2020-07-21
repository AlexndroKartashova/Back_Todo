using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Contacts
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetList(String userId, int? parentId);

        Task Add(Item item);
    }
}

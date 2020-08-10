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

        Task<Item> Get(String userId, int id);

        Task Add(Item item);

        Task EditItem(Item item);
    }
}

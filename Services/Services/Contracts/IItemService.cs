using Services.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Contacts
{
    public interface IItemService
    {
        Task<IEnumerable<ItemDto>> GetList(String userId, int? parentItemId = null);

        Task Add(ItemDto itemDto, string userId);
    }
}

using Services.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Contracts
{
    public interface IItemService
    {
        Task<IEnumerable<ItemDto>> GetList(String userId, int? parentItemId = null);

        Task Add(ItemDto itemDto, string userId);

        Task EditItem(ItemDto itemDto, string userId);

        Task ChangeStatus(ItemDto itemDto, string userId);

        Task DeleteItem(int id, string userId);

        Task<ItemDto> Get(string value, int id);
    }
}

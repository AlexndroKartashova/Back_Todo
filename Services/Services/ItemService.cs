using Services.Dtos;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Data.Repositories.Contacts;
using Services.Services.Contacts;

namespace Services.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task Add(ItemDto itemDto, string userId)
        {
            var item = new Item
            {
                UserId = userId,
                Name = itemDto.Name,
                ParentItemId = itemDto.ParentItemId
            };

            await _itemRepository.Add(item);
        }

        public async Task<IEnumerable<ItemDto>> GetList(string userId, int? parentItemId = null)
        {
            var items = await _itemRepository.GetList(userId, parentItemId);

            return items.Select(item => new ItemDto { Id = item.Id, Name = item.Name, IsDone = item.IsDone });
        }
    }
}

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

        public async Task EditItem(ItemDto itemDto, string userId)
        {
           var item = await _itemRepository.Get(userId, itemDto.Id);

            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            item.Name = itemDto.Name;
            item.IsDone = itemDto.IsDone;
           
            await _itemRepository.EditItem(item);
        }

        public async Task DeleteItem(int id, string userId)
        {
            var item = await _itemRepository.Get(userId, id);

            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            item.IsDeleted = true;

            await _itemRepository.EditItem(item);
        }
    }
}

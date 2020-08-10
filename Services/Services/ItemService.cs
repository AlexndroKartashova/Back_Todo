using Services.Dtos;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Data.Repositories.Contacts;
using Services.Services.Contracts;

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

            var itemForDel = await _itemRepository.Get(userId, item.Id);
            await EditStatusParent(itemForDel, userId);
        }

        public async Task<IEnumerable<ItemDto>> GetList(string userId, int? parentItemId = null)
        {
            var items = await _itemRepository.GetList(userId, parentItemId);

            var itemDtos = new List<ItemDto>();

            foreach (var item in items)
            {
                var childItems = await _itemRepository.GetList(userId, item.Id);

                itemDtos.Add(new ItemDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    IsDone = item.IsDone,
                    ParentItemId = item.ParentItemId,
                    ItemCount = childItems.Count(),
                    DoneItemCount = childItems.Count(x => x.IsDone)
                });
            }

            return itemDtos.OrderBy(ord => ord.Name);
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

            var itemForDel =  await _itemRepository.Get(userId, item.Id);

            await _itemRepository.EditItem(item);
            await EditStatusParent(itemForDel, userId);

        }

        public async Task<ItemDto> Get(string value, int id)
        {
            var item = await _itemRepository.Get(value, id);

            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return new ItemDto
            {
                Id = item.Id,
                ParentItemId = item.ParentItemId,
                Name = item.Name,
                IsDeleted = item.IsDeleted,
                IsDone = item.IsDone
            };
        }

        public async Task ChangeStatus(ItemDto itemDto, string userId)
        {
            var item = await _itemRepository.Get(userId, itemDto.Id);

            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            item.IsDone = itemDto.IsDone;
            await _itemRepository.EditItem(item);

            await EditStatusChildren(item, userId);
            await EditStatusParent(item, userId);
        }

        private async Task EditStatusChildren(Item item, String userId)
        {
            var childItems = await _itemRepository.GetList(userId, item.Id);

            foreach (var childItem in childItems)
            {
                childItem.IsDone = item.IsDone;
                await _itemRepository.EditItem(item);
                await EditStatusChildren(childItem, userId);
            }

        }

        private async Task EditStatusParent(Item item, String userId)
        {
            var itemsOneParent = await _itemRepository.GetList(userId, item.ParentItemId);

            bool itemsStatus = true;

            foreach (var itemOneParent in itemsOneParent)
            {
                itemsStatus = itemsStatus && itemOneParent.IsDone;
            }

            if (item.ParentItemId.HasValue)
            {
                var parentItem = await _itemRepository.Get(userId, item.ParentItemId.Value);
                parentItem.IsDone = itemsStatus;

                await _itemRepository.EditItem(item);
                await EditStatusParent(parentItem, userId);
            }
        }
    }
}

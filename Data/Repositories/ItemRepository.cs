using Data.Repositories.Contacts;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    class ItemRepository : IItemRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ItemRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task Add(Item item)
        {
            await _applicationDbContext.Set<Item>().AddAsync(item);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Item>> GetList(string userId, int? parentItemId)
        {
            return await _applicationDbContext.Set<Item>()
                .Where(x => x.UserId.Equals(userId) && x.ParentItemId == parentItemId)
                .ToListAsync();
        }
    }
}

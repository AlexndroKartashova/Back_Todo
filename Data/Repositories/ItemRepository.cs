using Data.Repositories.Contacts;
using Microsoft.AspNetCore.Mvc;
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

        public async Task EditItem(Item item)
        {
            _applicationDbContext.Set<Item>().Update(item);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<Item> Get(string userId, int id)
        {
            return await _applicationDbContext.Set<Item>()
                .FirstOrDefaultAsync(x => x.UserId.Equals(userId) && x.Id == id && !x.IsDeleted);
        }

        public async Task<IEnumerable<Item>> GetList(string userId, int? parentItemId)
        {
              return await _applicationDbContext.Set<Item>()
                .Where(x => x.UserId.Equals(userId) && x.ParentItemId == parentItemId && !x.IsDeleted)
                .ToListAsync();
        }

        public async Task IsDeleted(Item item)
        {
            _applicationDbContext.Set<Item>().Update(item);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}

using Data.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    class ContactRepository : IContactRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ContactRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task Add(Contact contact)
        {
            await _applicationDbContext.Set<Contact>().AddAsync(contact);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task EditContact(Contact contact)
        {
            _applicationDbContext.Set<Contact>().Update(contact);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<Contact> Get(string userId, int id)
        {
            return await _applicationDbContext.Set<Contact>()
                .FirstOrDefaultAsync(x => x.UserId.Equals(userId) && x.Id == id && !x.IsDeleted);
        }

        public async Task<IEnumerable<Contact>> GetList(string userId)
        {
            return await _applicationDbContext.Set<Contact>()
                .Where(x => x.UserId.Equals(userId) && !x.IsDeleted)
                .ToListAsync();
        }

        public async Task DeleteItem(Contact contact)
        {
            _applicationDbContext.Set<Contact>().Update(contact);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}

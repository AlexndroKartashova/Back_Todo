using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Contracts
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetList(String userId);

        Task<Contact> Get(String userId, int id);

        Task Add(Contact contact);

        Task EditContact(Contact contact);

    }
}

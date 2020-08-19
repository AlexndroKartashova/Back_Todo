using Services.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Contracts
{
    public interface IContactService
    {
        Task<IEnumerable<ContactDto>> GetList(String userId);

        Task Add(ContactDto contactDto, string userId);

        Task EditContact(ContactDto contactDto, string userId);

        Task DeleteContact(int id, string userId);

        Task<ContactDto> Get(string userId, int id);
    }
}

using Data.Repositories.Contracts;
using Models;
using Services.Dtos;
using Services.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ContactService : IContactService
    {

        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task Add(ContactDto contactDto, string userId)
        {
            var contact = new Contact
            {
                UserId = userId,
                Name = contactDto.Name,
                Email = contactDto.Email,
                PhoneNumber = contactDto.PhoneNumber,
            };

            await _contactRepository.Add(contact);
        }
        public async Task DeleteContact(int id, string userId)
        {
            var contact = await _contactRepository.Get(userId, id);

            if (contact == null)
            {
                throw new ArgumentNullException(nameof(contact));
            }

            contact.IsDeleted = true;
            await _contactRepository.EditContact(contact);
        }

        public async Task EditContact(ContactDto contactDto, string userId)
        {
            var contact = await _contactRepository.Get(userId, contactDto.Id);

            if (contact == null)
            {
                throw new ArgumentNullException(nameof(contact));
            }

            contact.Name = contactDto.Name;
            contact.PhoneNumber = contact.PhoneNumber;
            contact.Email = contact.Email;

            await _contactRepository.EditContact(contact);
        }

        public async Task<ContactDto> Get(string userId, int id)
        {
            var contact = await _contactRepository.Get(userId, id);

            if (contact == null)
            {
                throw new ArgumentNullException(nameof(contact));
            }

            return new ContactDto
            {
                Id = contact.Id,
                Name = contact.Name,
                PhoneNumber = contact.PhoneNumber,
                Email = contact.Email,
                IsDeleted = contact.IsDeleted,
                UserId = contact.UserId
            };
        }

        public async Task<IEnumerable<ContactDto>> GetList(string userId)
        {
            var contacts = await _contactRepository.GetList(userId);

            var contactDto = new List<ContactDto>();

            foreach (var contact in contacts)
            {
                contactDto.Add(new ContactDto
                {
                    Id = contact.Id,
                    Name = contact.Name,
                    PhoneNumber = contact.PhoneNumber,
                    Email = contact.Email,
                    UserId = contact.UserId
                });
            }

            return contactDto.OrderBy(ord => ord.Name);

        }
    }
}

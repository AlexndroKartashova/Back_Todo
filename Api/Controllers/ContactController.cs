using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update;
using Services.Dtos;
using Services.Services;
using Services.Services.Contracts;

namespace Api.Controllers
{
    [Route("api/contacts")]
    [ApiController]
    [Authorize]
    public class ContactController : CustomController
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet((""))]
        public async Task<ActionResult> GetContacts()
        {
            var contactDtos = await _contactService.GetList(GetUserId());

            var contactModel = contactDtos.Select(contact => new ContactModel
            {
                Id = contact.Id,
                Name = contact.Name,
                PhoneNumber = contact.PhoneNumber,
                Email = contact.Email,
                UserId = contact.UserId
            });
            return Ok(contactModel);
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var contactDto = await _contactService.Get(GetUserId(), id);

            var contactModel = new ContactModel
            {
                Id = contactDto.Id,
                Name = contactDto.Name,
                IsDeleted = contactDto.IsDeleted,
                PhoneNumber =contactDto.PhoneNumber,
                Email = contactDto.Email,
                UserId = contactDto.UserId
            };

            return Ok(contactModel);

        }

        [HttpPost("add")]
        public async Task<ActionResult> Add([FromBody] ContactModel contactModel)
        {
            if (ModelState.IsValid)
            {
                var contactDto = new ContactDto
                {
                    Name = contactModel.Name,
                    UserId = contactModel.UserId,
                    PhoneNumber = contactModel.PhoneNumber,
                    Email = contactModel.Email
                };

                await _contactService.Add(contactDto, GetUserId());
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("edit")]
        public async Task<ActionResult> Edit([FromBody] ContactModel contactModel)
        {
            var contactDto = new ContactDto
            {
                Id = contactModel.Id,
                Name = contactModel.Name,
                IsDeleted = contactModel.IsDeleted,
                PhoneNumber = contactModel.PhoneNumber,
                Email = contactModel.Email
            };

            await _contactService.EditContact(contactDto, GetUserId());
            return Ok();
        }

        [HttpGet("{id}/delete")]
        public async Task<ActionResult> Delete(int id)
        {
            await _contactService.DeleteContact(id, GetUserId());

            return Ok();
        }
    }
}

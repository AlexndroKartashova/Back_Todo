using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Dtos;
using Services.Services.Contracts;

namespace Api.Controllers
{
    [Route("api/profile")]
    [ApiController]
    [Authorize]
    public class UserController : CustomController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService
            )
        {
            _userService = userService;
        }

        [HttpGet("")]
        public async Task<ActionResult> GetUserById()
        {
            var userDto = await _userService.GetUserById(GetUserId());
            var userModel = new UserModel
            {
                Id = userDto.Id,
                Email = userDto.Email,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Birthday = userDto.Birthday,
                PhoneNumber = userDto.PhoneNumber
            };

            return Ok(userModel);
        }
        

        [HttpPost("edit")]
        public async Task<ActionResult> EditUser([FromBody] UserModel editUserModel)
        {
            var userDto = new UserDto
            {
                Email = editUserModel.Email,
                FirstName = editUserModel.FirstName,
                LastName = editUserModel.LastName,
                Birthday = editUserModel.Birthday,
                Id = editUserModel.Id,
                PhoneNumber = editUserModel.PhoneNumber
            };

            await _userService.EditUser(userDto, GetUserId());
            return Ok();
        }
    }
}

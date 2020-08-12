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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        private string GetUserId()
        {
            var claim = User.Claims.FirstOrDefault(x => x.Type.Equals("id"));

            if (claim == null)
            {
                throw new ArgumentNullException(nameof(claim));
            }

            return claim.Value;
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
                Birthday = userDto.Birthday
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
                Id = editUserModel.Id
            };

            await _userService.EditUser(userDto, GetUserId());
            return Ok();
        }
    }
}

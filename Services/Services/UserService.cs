using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using Services.Dtos;
using Services.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager; 
        }

        
        public async Task EditUser(UserDto userDto, string id)
        {
           var user = await _userManager.FindByIdAsync(id);

            if (user == null)
           {
               throw new ArgumentNullException(nameof(user));
           }

            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Birthday = userDto.Birthday;
            //user.Email = userDto.Email;

            await _userManager.UpdateAsync(user);
        }
        


        public async Task<UserDto> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                throw new ArgumentNullException();
            }

            return new UserDto
            {
                Email = user.Email,
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Birthday = user.Birthday
            };
        }

    }
}

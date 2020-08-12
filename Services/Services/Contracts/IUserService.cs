using Services.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Contracts
{
    public interface IUserService
    {
        //Task Get(UserDto UsermDto, string userId);
        //Task<UserDto> GetUserById(string id);
        //Task EditUser(UserDto UsermDto, string id);

        Task<UserDto> GetUserById(string id);
        Task EditUser(UserDto userDto, string v);

        //Task<UserDto> GetByEmail(string email);
    }
}

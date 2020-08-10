using Services.Dtos;
using System.Threading.Tasks;
using System;
using System.Text;

namespace Services.Services.Contracts
{
    public interface IAuthService
    {
        Task<bool> Register(RegisterDto registerDto);

        Task<bool> Login(LoginDto loginDto);

        Task<UserDto> GetUserByEmail(string email);

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Api.Infrastructure;
using Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models;
using Services.Dtos;
using Services.Services;
using Services.Services.Contracts;

namespace Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel LoginModel)
        {
            if (ModelState.IsValid)
            {
                var loginDto = new LoginDto
                {
                    Email = LoginModel.Email,
                    Password = LoginModel.Password
                };

                if (await _authService.Login(loginDto))
                {
                    var user = await _authService.GetUserByEmail(loginDto.Email);
                    var response =  GenerateToken(user);
                    return Ok(response);
                }
            }
            return Unauthorized();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel RegisterModel)
        {

            if (ModelState.IsValid)
            {
                var registerDto = new RegisterDto
                {
                    Email = RegisterModel.Email,
                    Password = RegisterModel.Password,
                    ConfirmPassword = RegisterModel.ConfirmPassword
                };

                if (await _authService.Register(registerDto))
                {
                    var user = await _authService.GetUserByEmail(registerDto.Email);
                    var response = GenerateToken(user);
                    return Ok(response);
                }
            }
            return Unauthorized();
        }

        private object GenerateToken(UserDto user)
        {
            {
                var identity = GetIdentity(user);
                var now = DateTime.UtcNow;
                var expiredDate = now.Add(TimeSpan.FromMinutes(TokenApp.LIFETIME));

                var jwt = new JwtSecurityToken(
                    issuer: TokenApp.ISSUER,
                    audience: TokenApp.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: expiredDate,
                    signingCredentials: new SigningCredentials(TokenApp.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                var response = new
                {
                    encodedJwt,
                    expiredDate
                };

                return response;
            }
        }

        private ClaimsIdentity GetIdentity(UserDto user)
        {
            var claims = new List<Claim>
            {
                new Claim("email", user.Email),
                new Claim("id", user.Id)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }
}

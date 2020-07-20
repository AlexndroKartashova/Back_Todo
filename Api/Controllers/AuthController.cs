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

namespace Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [EnableCors("Cors")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthController(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel LoginModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(LoginModel.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, LoginModel.Password, LoginModel.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        var response = GenerateToken(user);

                        return Ok(response);
                    }
                }
            }

            return Unauthorized();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel RegisterModel)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = RegisterModel.Email,
                    Email = RegisterModel.Email
                };

                var result = await _userManager.CreateAsync(user, RegisterModel.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    var response = GenerateToken(user);
                    return Ok(response);

                }
            }

            return Unauthorized();
        }

        private object GenerateToken(IdentityUser user)
        {
            {
                var identity = GetIdentity(user);
                var now = DateTime.UtcNow;
                var expiredDate = now.Add(TimeSpan.FromMinutes(TokenApp.LIFETIME));

                //create jwt-token
                var jwt = new JwtSecurityToken(
                    issuer: TokenApp.ISSUER,
                    audience: TokenApp.AUDIENCE,
                    notBefore: now, //time now for activated serteficats
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

        private ClaimsIdentity GetIdentity(IdentityUser user)
        {
            var claims = new List<Claim>
                                {
                                    new Claim("email", user.Email),
                                    new Claim("id", user.Id)
                                };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims);
            return claimsIdentity;
        }

        [Authorize]
        [HttpGet("test")]
        //[HttpPost("add")]
        public ActionResult Test()
        {
            return Ok("Good request");
        }
    }
}

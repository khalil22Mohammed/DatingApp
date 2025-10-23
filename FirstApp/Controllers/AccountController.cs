using FirstApp.Data;
using FirstApp.DTOs;
using FirstApp.Entities;
using FirstApp.Extensions;
using FirstApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace FirstApp.Controllers
{
    public class AccountController (AppDbContext context,ItokenService itokenService): BaesAPIController
    {
        [HttpPost("register")] // api/account/register

        public async Task<ActionResult<UserDto>> Register(RegisterDTO registerDTO)
        {
            if (await EmailExists(registerDTO.Email)) return BadRequest("Email taken");
           using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                DisplayName = registerDTO.DisplayName,
                Email = registerDTO.Email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
                PasswordSalt =hmac.Key

            };
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user.ToDto(itokenService);


        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDTO loginDTO)
        {
            var User = await context.Users.SingleOrDefaultAsync(x => x.Email == loginDTO.Email);

            if (User == null) return Unauthorized("Invalid email address");

            using var hmac = new HMACSHA512(User.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.password));

            for(var i =0;i < computedHash.Length; i++)
            {

                if (computedHash[i] != User.PasswordHash[i]) return Unauthorized("Invalid password");
            }
            return User.ToDto(itokenService); 
        }   
        private async Task<bool>EmailExists(string email)
        {
            return await context.Users.AnyAsync(x => x.Email.ToLower() == email.ToLower());

        }
    }

}

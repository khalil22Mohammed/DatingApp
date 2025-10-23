using FirstApp.DTOs;
using FirstApp.Entities;
using FirstApp.Interfaces;
using FirstApp.Services;

namespace FirstApp.Extensions
{
    public static class UserAppExtensions
    {
        public static UserDto ToDto(this AppUser user, ItokenService itokenService)
        {

            return new UserDto
            {

                DisplayName = user.DisplayName,
                Email = user.Email,
                Id = user.ID,
                token = itokenService.CreateToken(user)
            };
        }   
    }
}

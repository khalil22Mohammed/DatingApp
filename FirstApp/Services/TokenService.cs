using FirstApp.Entities;
using FirstApp.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FirstApp.Services
{
    public class TokenService(IConfiguration configurations) : ItokenService
    {
        public string CreateToken(AppUser user)
        {
            var tokenkey = configurations["TokenKey"] ?? throw new Exception(" Connot Get Token key");

            if (tokenkey.Length < 64) 
            {
                throw new Exception("Your token key needs to be >= 64 characters")
                {

                };
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenkey));

            var Claims = new List<Claim>
            {   new (ClaimTypes.NameIdentifier, user.ID),
                new (ClaimTypes.Email, user.Email),
                
            };
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(Claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

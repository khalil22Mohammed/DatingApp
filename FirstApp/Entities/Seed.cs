using FirstApp.Data;
using FirstApp.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace FirstApp.Entities
{
    public class Seed
    {
        public static async Task SeedUsers(AppDbContext context)
        {
            if (await context.Users.AnyAsync()) return;
            var memberData = await File.ReadAllTextAsync("Data/UserSeedData.json");
            var members = JsonSerializer.Deserialize<List<SeedUserDto>>(memberData);

            if (members == null)
            {
                Console.WriteLine("No member data found to seed.");
                return;
            }
            using var hmac = new HMACSHA512();

            foreach (var member in members)
            {
                var user = new AppUser
                {
                    ID = member.Id,
                    Email = member.Email,
                    DisplayName = member.DisplayName,
                    ImageUrl = member.ImageUrl,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd")),
                    PasswordSalt = hmac.Key,
                    Member = new Member
                    {
                        Id = member.Id,
                        DisplayName = member.DisplayName,
                        Description = member.Description,
                        ImageUrl = member.ImageUrl,
                        DateOfBirth = member.DateOfBirth,
                        Gender =member.Gender,
                        City = member.City,
                        Country = member.Country,
                        Created = member.Created,
                        LastActive = member.LastActive,
                    }
                };

                user.Member.Photos.Add(new photo
                {
                    Url = member.ImageUrl !,

                    MemberId = member.Id

                });
                context.Users.Add(user);
            }
            await context.SaveChangesAsync();
        }
    }
}

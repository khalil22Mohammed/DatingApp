using FirstApp.Data;
using FirstApp.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstApp.Controllers
{
    
    public class MembersController(AppDbContext context) : BaesAPIController
    {
        [HttpGet]
        public async Task< ActionResult<IReadOnlyList<AppUser>>> GetMembers()
        {
            var members = await context.Users.ToListAsync();
            return members;

        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task< ActionResult<AppUser>> GetMember(string id)
        {
            var members = await context.Users.FindAsync(id);
            if (members == null) return NotFound();
            return members;

        }


    }
}

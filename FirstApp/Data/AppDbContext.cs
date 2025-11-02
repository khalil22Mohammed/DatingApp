using FirstApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirstApp.Data
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)

    {
        public DbSet<AppUser> Users { get; set; }

        public DbSet<Member> Members { get; set; }

        public DbSet<photo> Photos { get; set; }
    }
}

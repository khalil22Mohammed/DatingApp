using FirstApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirstApp.Data
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)

    {
        public DbSet<AppUser> Users { get; set; }

        public DbSet<Member> Members { get; set; }

        public DbSet<Photo> Photos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Member.Id as string
            modelBuilder.Entity<Member>()
                .Property(m => m.Id)
                .HasColumnType("nvarchar(450)");

            // Configure AppUser.ID as string
            modelBuilder.Entity<AppUser>()
                .Property(u => u.ID)
                .HasColumnType("nvarchar(450)");

            // Configure Photo.MemberId as string
            modelBuilder.Entity<Photo>()
                .Property(p => p.MemberId)
                .HasColumnType("nvarchar(450)");
        }
    }
}

using OnlineQuizz.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OnlineQuizz.Identity
{
    public class OnlineQuizzIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
      
        public OnlineQuizzIdentityDbContext(DbContextOptions<OnlineQuizzIdentityDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(rt => rt.Id);

                entity.Property(rt => rt.Token)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasIndex(rt => rt.Token)
                    .IsUnique();

                entity.HasOne(rt => rt.User)
                    .WithMany(u => u.RefreshTokens)
                    .HasForeignKey(rt => rt.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}

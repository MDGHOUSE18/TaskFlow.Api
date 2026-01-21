using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Api.DTOs.Entities;

namespace TaskFlow.Api.Infrastructure.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Identity tables
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<IdentityUserLogin<string>> UserLogins { get; set; } = null!;
        public DbSet<IdentityUserToken<string>> UserTokens { get; set; } = null!;
        public DbSet<IdentityUserClaim<string>> UserClaims { get; set; } = null!;

        // Domain tables
        public DbSet<TaskItem> TaskItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // User configuration
            builder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.Property(u => u.Name)
                      .IsRequired()
                      .HasMaxLength(100);
            });

            // TaskItem configuration
            builder.Entity<TaskItem>(entity =>
            {
                entity.ToTable("Tasks");

                entity.HasKey(t => t.Id);

                entity.Property(t => t.Title)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(t => t.Description)
                      .HasMaxLength(1000);

                entity.Property(t => t.CreatedAt)
                      .IsRequired();

                entity.Property(t => t.UpdatedAt)
                      .IsRequired();

                entity.HasOne(t => t.User)
                      .WithMany()
                      .HasForeignKey(t => t.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
            // ---------- Identity key configurations ----------

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
                entity.HasKey(l => new { l.LoginProvider, l.ProviderKey });
            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
                entity.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
                entity.HasKey(c => c.Id);
            });
        }


    }
}

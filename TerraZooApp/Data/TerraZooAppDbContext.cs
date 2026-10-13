using TerraZooApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace TerraZooApp.Data
{
    public class TerraZooAppDbContext : IdentityDbContext<Users>
    {
        public TerraZooAppDbContext(DbContextOptions<TerraZooAppDbContext> options)
            : base(options)
        { }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<TourGuideSchedule> TourGuideSchedules { get; set; }
        public DbSet<TerraZooSchedule> TerraZooSchedules { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<Users>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("AspNetRoles");
            
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("AspNetUserRoles");
                entity.HasKey(ur => new { ur.UserId, ur.RoleId });
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("AspNetUserClaims");
                entity.HasKey(uc => uc.Id);
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("AspNetUserLogins");
                entity.HasKey(l => new { l.LoginProvider, l.ProviderKey });
            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("AspNetRoleClaims");
                entity.HasKey(rc => rc.Id);
            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("AspNetUserTokens");
                entity.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
            });
        }
    }
}

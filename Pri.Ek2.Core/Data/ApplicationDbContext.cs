using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pri.Ek2.Core.Entities;

namespace Pri.Ek2.Core.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<MaintenanceLog> MaintenanceLogs { get; set; }
        public DbSet<MaintenanceSchedule> MaintenanceSchedules { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Identity tabelnamen
            modelBuilder.Entity<IdentityUser>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");

            // UserProfile configuratie
            modelBuilder.Entity<UserProfile>()
                .HasOne(up => up.IdentityUser)
                .WithMany()
                .HasForeignKey(up => up.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // MaintenanceLog configuratie
            modelBuilder.Entity<MaintenanceLog>()
                .HasOne(ml => ml.Vehicle)
                .WithMany(v => v.MaintenanceLogs)
                .HasForeignKey(ml => ml.VehicleId);

            // ✅ Precision voor decimal velden
            modelBuilder.Entity<Vehicle>()
                .Property(v => v.EmissionFactor)
                .HasPrecision(10, 5);

            modelBuilder.Entity<MaintenanceLog>()
                .Property(ml => ml.NewEmissionFactor)
                .HasPrecision(10, 5);
        }
    }
}

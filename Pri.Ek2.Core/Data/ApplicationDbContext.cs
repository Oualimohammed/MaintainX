using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pri.Ek2.Core.Entities;

namespace Pri.Ek2.Core.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<TransportRoute> TransportRoutes { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<EmissionGoal> EmissionGoals { get; set; }
        public DbSet<EmissionReport> EmissionReports { get; set; }
        public DbSet<MaintenanceLog> MaintenanceLogs { get; set; }
        public DbSet<Reward> Rewards { get; set; }

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

            // TransportRoute configuratie
            modelBuilder.Entity<TransportRoute>()
                .HasOne(tr => tr.Vehicle)
                .WithMany(v => v.TransportRoutes)
                .HasForeignKey(tr => tr.VehicleId);

            modelBuilder.Entity<TransportRoute>()
                .HasOne(tr => tr.StartLocation)
                .WithMany(l => l.StartRoutes)
                .HasForeignKey(tr => tr.StartLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TransportRoute>()
                .HasOne(tr => tr.EndLocation)
                .WithMany(l => l.EndRoutes)
                .HasForeignKey(tr => tr.EndLocationId)
                .OnDelete(DeleteBehavior.Restrict);

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

            // EmissionGoal configuratie
            modelBuilder.Entity<EmissionGoal>()
                .HasOne(eg => eg.UserProfile)
                .WithMany(up => up.EmissionGoals)
                .HasForeignKey(eg => eg.UserId)
                .HasPrincipalKey(up => up.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // EmissionReport configuratie
            modelBuilder.Entity<EmissionReport>()
                .HasOne(er => er.UserProfile)
                .WithMany(up => up.EmissionReports)
                .HasForeignKey(er => er.UserId)
                .HasPrincipalKey(up => up.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
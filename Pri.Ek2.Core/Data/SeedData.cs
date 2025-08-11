using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Pri.Ek2.Core.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Pri.Ek2.Core.Data
{
    public class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            // 1. Seed rollen
            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole("Admin"));

            if (!await roleManager.RoleExistsAsync("Mechanic"))
                await roleManager.CreateAsync(new IdentityRole("Mechanic"));

            // 2. Seed standaardgebruikers
            if (!context.Users.Any())
            {
                var adminUser = new IdentityUser
                {
                    UserName = "admin@example.com",
                    Email = "admin@example.com",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(adminUser, "AdminPassword123!");
                await userManager.AddToRoleAsync(adminUser, "Admin");


                var mechanicUser = new IdentityUser
                {
                    UserName = "mechanic@example.com",
                    Email = "mechanic@example.com",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(mechanicUser, "MechanicPassword123!");
                await userManager.AddToRoleAsync(mechanicUser, "Mechanic");

                // UserProfiles toevoegen
                context.UserProfiles.AddRange(
                    new UserProfile
                    {
                        UserId = adminUser.Id,
                        FirstName = "Admin",
                        LastName = "User",
                        BirthData = new DateTime(1980, 1, 1),
                        Email = adminUser.Email,
                        ProfileImagePath = "images/admin.png"
                    },
               
                    new UserProfile
                    {
                        UserId = mechanicUser.Id,
                        FirstName = "John",
                        LastName = "Doe",
                        BirthData = new DateTime(1985, 5, 15),
                        Email = mechanicUser.Email,
                        ProfileImagePath = "images/mechanic.png"
                    }
                );
                await context.SaveChangesAsync();
            }

            // 3. Seed voertuigen
            if (!context.Vehicles.Any())
            {
                context.Vehicles.AddRange(
                    new Vehicle { LicensePlate = "ABC123", Model = "Toyota Prius", Type = VehicleType.Hybrid, EmissionFactor = 0.089m },
                    new Vehicle { LicensePlate = "XYZ789", Model = "Tesla Model 3", Type = VehicleType.Electric, EmissionFactor = 0.0m },
                    new Vehicle { LicensePlate = "LMN456", Model = "Ford F-150", Type = VehicleType.Gasoline, EmissionFactor = 0.250m },
                    new Vehicle { LicensePlate = "DEF321", Model = "Honda Accord", Type = VehicleType.Hybrid, EmissionFactor = 0.080m },
                    new Vehicle { LicensePlate = "GHI654", Model = "Chevrolet Bolt", Type = VehicleType.Electric, EmissionFactor = 0.0m },
                    new Vehicle { LicensePlate = "JKL987", Model = "Volkswagen Golf", Type = VehicleType.Gasoline, EmissionFactor = 0.120m },
                    new Vehicle { LicensePlate = "MNO234", Model = "Nissan Leaf", Type = VehicleType.Electric, EmissionFactor = 0.0m },
                    new Vehicle { LicensePlate = "PQR567", Model = "BMW i3", Type = VehicleType.Electric, EmissionFactor = 0.0m },
                    new Vehicle { LicensePlate = "STU890", Model = "Audi A3", Type = VehicleType.Hybrid, EmissionFactor = 0.075m },
                    new Vehicle { LicensePlate = "VWX345", Model = "Mercedes-Benz C-Class", Type = VehicleType.Gasoline, EmissionFactor = 0.150m },
                    new Vehicle { LicensePlate = "YZA678", Model = "Hyundai Kona Electric", Type = VehicleType.Electric, EmissionFactor = 0.0m }
                );
                await context.SaveChangesAsync();
            }


            // 6. Seed onderhoudslogs (moet na voertuigen)
            if (!context.MaintenanceLogs.Any() && context.Vehicles.Any())
            {
                context.MaintenanceLogs.AddRange(
                    new MaintenanceLog
                    {
                        VehicleId = 1,
                        MaintenanceDate = DateTime.Now.AddMonths(-1),
                        Description = "Olie vervangen",
                        NewEmissionFactor = 0.085m
                    },
                    new MaintenanceLog
                    {
                        VehicleId = 3,
                        MaintenanceDate = DateTime.Now.AddMonths(-2),
                        Description = "Bandenspanning aangepast"
                    },
                    new MaintenanceLog
                    {
                        VehicleId = 2,
                        MaintenanceDate = DateTime.Now.AddMonths(-3),
                        Description = "Software update uitgevoerd",
                        NewEmissionFactor = 0.0m
                    },
                    new MaintenanceLog
                    {
                        VehicleId = 4,
                        MaintenanceDate = DateTime.Now.AddMonths(-1),
                        Description = "Remmen gecontroleerd",
                        NewEmissionFactor = 0.078m
                    },
                    new MaintenanceLog
                    {
                        VehicleId = 5,
                        MaintenanceDate = DateTime.Now.AddMonths(-2),
                        Description = "Accu gereviseerd",
                        NewEmissionFactor = 0.0m
                    },
                    new MaintenanceLog
                    {
                        VehicleId = 6,
                        MaintenanceDate = DateTime.Now.AddMonths(-1),
                        Description = "Motor gerepareerd",
                        NewEmissionFactor = 0.115m
                    },
                    new MaintenanceLog
                    {
                        VehicleId = 7,
                        MaintenanceDate = DateTime.Now.AddMonths(-3),
                        Description = "Banden vervangen",
                        NewEmissionFactor = 0.0m
                    },
                    new MaintenanceLog
                    {
                        VehicleId = 8,
                        MaintenanceDate = DateTime.Now.AddMonths(-2),
                        Description = "Software update uitgevoerd",
                        NewEmissionFactor = 0.0m
                    },
                    new MaintenanceLog
                    {
                        VehicleId = 9,
                        MaintenanceDate = DateTime.Now.AddMonths(-1),
                        Description = "Remmen gecontroleerd",
                        NewEmissionFactor = 0.072m
                    },
                    new MaintenanceLog
                    {
                        VehicleId = 10,
                        MaintenanceDate = DateTime.Now.AddMonths(-2),
                        Description = "Olie vervangen",
                        NewEmissionFactor = 0.145m
                    },
                    new MaintenanceLog
                    {
                        VehicleId = 11,
                        MaintenanceDate = DateTime.Now.AddMonths(-1),
                        Description = "Accu gereviseerd",
                        NewEmissionFactor = 0.0m
                    }
                );
            }

            // 6. Seed onderhoudsschema's (moet na voertuigen)
            if (!context.MaintenanceSchedules.Any() && context.Vehicles.Any())
            {
                context.MaintenanceSchedules.AddRange(
                    new MaintenanceSchedule
                    {
                        VehicleId = 1,
                        LastMaintenanceDate = DateTime.Now.AddMonths(-3),
                        NextMaintenanceDueDate = DateTime.Now.AddMonths(3),
                        MileageAtLastMaintenance = 20000,
                        NextMaintenanceMileage = 30000,
                        Status = "Pending",
                        Notes = "Standaard onderhoud"
                    },
                    new MaintenanceSchedule
                    {
                        VehicleId = 2,
                        LastMaintenanceDate = DateTime.Now.AddMonths(-1),
                        NextMaintenanceDueDate = DateTime.Now.AddMonths(5),
                        MileageAtLastMaintenance = 10000,
                        NextMaintenanceMileage = 20000,
                        Status = "Pending",
                        Notes = "Elektrisch voertuig"
                    }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}
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
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                await roleManager.CreateAsync(new IdentityRole("User"));
                await userManager.CreateAsync(new IdentityUser
                    ("Mechanic"));
            }

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

                var normalUser = new IdentityUser
                {
                    UserName = "user@example.com",
                    Email = "user@example.com",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(normalUser, "UserPassword123!");
                await userManager.AddToRoleAsync(normalUser, "User");

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
                        UserId = normalUser.Id,
                        FirstName = "Normal",
                        LastName = "User",
                        BirthData = new DateTime(1990, 1, 1),
                        Email = normalUser.Email,
                        ProfileImagePath = "images/user.png"
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
                    new Vehicle { LicensePlate = "LMN456", Model = "Ford F-150", Type = VehicleType.Gasoline, EmissionFactor = 0.250m }
                );
                await context.SaveChangesAsync();
            }

            // 4. Seed locaties
            if (!context.Locations.Any())
            {
                context.Locations.AddRange(
                    new Location
                    {
                        Name = "Howest Brugge",
                        Address = "Rijselstraat 5, 8200 Brugge",
                        Latitude = 51.1874,
                        Longitude = 3.2162
                    },
                    new Location
                    {
                        Name = "Howest Kortrijk",
                        Address = "Graaf Karel de Goedelaan 5, 8500 Kortrijk",
                        Latitude = 50.8237,
                        Longitude = 3.2642
                    }
                );
                await context.SaveChangesAsync();
            }

            // 5. Seed transportroutes
            if (!context.TransportRoutes.Any() && context.Vehicles.Any() && context.Locations.Any())
            {
                var vehicles = context.Vehicles.ToList();
                var locations = context.Locations.ToList();

                context.TransportRoutes.AddRange(
                    new TransportRoute
                    {
                        StartLocationId = locations[0].Id,
                        EndLocationId = locations[1].Id,
                        DistanceKm = 45.7m,
                        EstimatedEmissionsKg = vehicles[0].EmissionFactor * 45.7m,
                        VehicleId = vehicles[0].Id,
                        ProofPath = "routes/route1.pdf"
                    }
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
                    }
                );
            }

            // 7. Seed emissiedoelen (moet na users)
            if (!context.EmissionGoals.Any() && context.UserProfiles.Any())
            {
                var user = context.UserProfiles.First();
                context.EmissionGoals.Add(
                    new EmissionGoal
                    {
                        UserId = user.UserId,
                        TargetEmissionsKg = 100,
                        StartDate = new DateTime(2024, 1, 1),
                        EndDate = new DateTime(2024, 12, 31),
                        CurrentEmissionsKg = 45
                    }
                );
            }

            // 8. Seed beloningen
            if (!context.Rewards.Any())
            {
                context.Rewards.AddRange(
                    new Reward
                    {
                        Name = "Groene Reisder",
                        Description = "Minder dan 50kg CO2 uitstoot deze maand",
                        IconPath = "rewards/green.png"
                    },
                    new Reward
                    {
                        Name = "Elektrische Kampioen",
                        Description = "10 ritten met een elektrisch voertuig",
                        IconPath = "rewards/electric.png"
                    }
                );
            }

            await context.SaveChangesAsync();
        }
    }
}
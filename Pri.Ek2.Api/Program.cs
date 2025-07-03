
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pri.Ek2.Core.Data;
using Pri.Ek2.Core.Services.Implementations;
using Pri.Ek2.Core.Services.Interfaces;

namespace Pri.Ek2.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add Services for Dependency Injection
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add services to the container.
            builder.Services.AddScoped<IVehicleService, VehicleService>();
            builder.Services.AddScoped<IEmissionReportService, EmissionReportService>();
            builder.Services.AddScoped<ITransportRouteService, TransportRouteService>();
            builder.Services.AddScoped<IEmissionGoalService, EmissionGoalService>();
            builder.Services.AddScoped<IUserProfileService, UserProfileService>();
            builder.Services.AddScoped<IRewardService, RewardService>();
            builder.Services.AddScoped<IMaintenanceLogService, MaintenanceLogService>();
            builder.Services.AddScoped<ILocationService, LocationService>();


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

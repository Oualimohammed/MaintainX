using Microsoft.EntityFrameworkCore;
using Pri.Ek2.Core.Data;
using Pri.Ek2.Core.Dtos.RequestDtos;
using Pri.Ek2.Core.Dtos.ResponseDtos;
using Pri.Ek2.Core.Entities;
using Pri.Ek2.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ek2.Core.Services.Implementations
{
    public class TransportRouteService : ITransportRouteService
    {
        private readonly ApplicationDbContext _context;

        public TransportRouteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TransportRouteResponseDto> AddAsync(TransportRouteRequestDto dto)
        {
            // Validatie
            if (!await _context.Locations.AnyAsync(l => l.Id == dto.StartLocationId))
                throw new ArgumentException("StartLocationId is invalid");

            if (!await _context.Vehicles.AnyAsync(v => v.Id == dto.VehicleId))
                throw new ArgumentException("VehicleId is invalid");


            // Bereken emissies
            var vehicle = await _context.Vehicles.FindAsync(dto.VehicleId);
            var emissions = vehicle.EmissionFactor * (decimal)dto.DistanceKm;

            var route = new TransportRoute
            {
                StartLocationId = dto.StartLocationId,
                EndLocationId = dto.EndLocationId,
                VehicleId = dto.VehicleId,
                DistanceKm = (decimal)dto.DistanceKm,
                EstimatedEmissionsKg = emissions,
                ProofPath = dto.ProofPath
            };

            _context.TransportRoutes.Add(route);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(route.Id); // Hergebruik de get-methode
        }

        public async Task DeleteAsync(int id)
        {
            var route = await _context.TransportRoutes.FindAsync(id);
            if (route == null)
                throw new KeyNotFoundException($"Route with ID {id} not found");

            _context.TransportRoutes.Remove(route);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TransportRouteResponseDto>> GetAllAsync()
        {
            return await _context.TransportRoutes
                .Include(r => r.StartLocation)
                .Include(r => r.EndLocation)
                .Include(r => r.Vehicle)
                .Select(r => MapToDto(r))
                .ToListAsync();
        }

        public async Task<TransportRouteResponseDto> GetByIdAsync(int id)
        {
            var route = await _context.TransportRoutes
                .Include(r => r.StartLocation)
                .Include(r => r.EndLocation)
                .Include(r => r.Vehicle)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (route == null)
                throw new KeyNotFoundException($"Route with ID {id} not found");

            return MapToDto(route);
        }

        public async Task<IEnumerable<TransportRouteResponseDto>> GetRoutesByVehicleAsync(int vehicleId)
        {
            return await _context.TransportRoutes
                .Where(r => r.VehicleId == vehicleId)
                .Include(r => r.StartLocation)
                .Include(r => r.Vehicle)
                .Select(r => MapToDto(r))
                .ToListAsync();
        }

        public async Task<TransportRouteResponseDto> UpdateAsync(int id, TransportRouteRequestDto dto)
        {
            var route = await _context.TransportRoutes.FindAsync(id);
            if (route == null)
                throw new KeyNotFoundException($"Route with ID {id} not found");

            // Update properties
            route.StartLocationId = dto.StartLocationId;
            route.EndLocationId = dto.EndLocationId;
            route.VehicleId = dto.VehicleId;
            route.DistanceKm = (decimal)dto.DistanceKm;
            route.ProofPath = dto.ProofPath;

            // Herbereken emissies
            var vehicle = await _context.Vehicles.FindAsync(dto.VehicleId);
            route.EstimatedEmissionsKg = vehicle.EmissionFactor * (decimal)dto.DistanceKm;

            await _context.SaveChangesAsync();
            return await GetByIdAsync(id);
        }

        // Hulpmethode voor mapping
        private TransportRouteResponseDto MapToDto(TransportRoute route)
        {
            return new TransportRouteResponseDto
            {
                Id = route.Id,
                StartLocation = new LocationResponseDto
                {
                    Id = route.StartLocation.Id,
                    Name = route.StartLocation.Name,
                    Address = route.StartLocation.Address,
                    Latitude = route.StartLocation.Latitude,
                    Longitude = route.StartLocation.Longitude
                },
                EndLocation = new LocationResponseDto
                {
                    Id = route.EndLocation.Id,
                    Name = route.EndLocation.Name,
                    Address = route.EndLocation.Address,
                    Latitude = route.EndLocation.Latitude,
                    Longitude = route.EndLocation.Longitude
                },
                Vehicle = new VehicleResponseDto
                {
                    Id = route.Vehicle.Id,
                    LicensePlate = route.Vehicle.LicensePlate,
                    Model = route.Vehicle.Model,
                    Type = route.Vehicle.Type,
                    EmissionFactor = route.Vehicle.EmissionFactor
                },
                DistanceKm = route.DistanceKm,
                EstimatedEmissionsKg = route.EstimatedEmissionsKg,
                ProofPath = route.ProofPath
            };
        }


        // dat is een methode die de TransportRoute entiteit omzet naar een TransportRouteResponseDto
        public async Task UpdateProofPathAsync(int id, string proofPath)
        {
            var route = await _context.TransportRoutes.FindAsync(id);
            if (route == null)
                throw new KeyNotFoundException($"Route with ID {id} not found");
            route.ProofPath = proofPath;
            await _context.SaveChangesAsync();
        }
    }
}

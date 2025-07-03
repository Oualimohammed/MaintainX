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
    public class VehicleService : IVehicleService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public VehicleService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public Task<VehicleResponseDto> AddAsync(VehicleRequestDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Vehicle request DTO cannot be null.");
            }
            var vehicle = new Vehicle
            {
                LicensePlate = dto.LicensePlate,
                Model = dto.Model,
                Type = dto.Type,
                EmissionFactor = dto.EmissionFactor
            };
            _applicationDbContext.Vehicles.Add(vehicle);
            _applicationDbContext.SaveChanges();
            return Task.FromResult(new VehicleResponseDto
            {
                Id = vehicle.Id,
                LicensePlate = vehicle.LicensePlate,
                Model = vehicle.Model,
                Type = vehicle.Type,
                EmissionFactor = vehicle.EmissionFactor
            });
        }

        public Task DeleteAsync(int id)
        {
            var vehicle = _applicationDbContext.Vehicles.Find(id);
            if (vehicle == null)
            {
                throw new KeyNotFoundException($"Vehicle with ID {id} not found.");
            }
            _applicationDbContext.Vehicles.Remove(vehicle);
            _applicationDbContext.SaveChanges();
            return Task.CompletedTask;
        }

        public Task<IEnumerable<VehicleResponseDto>> GetAllAsync()
        {
            var vehicles = _applicationDbContext.Vehicles
                .Select(v => new VehicleResponseDto
                {
                    Id = v.Id,
                    LicensePlate = v.LicensePlate,
                    Model = v.Model,
                    Type = v.Type,
                    EmissionFactor = v.EmissionFactor
                }).ToList();
            return Task.FromResult<IEnumerable<VehicleResponseDto>>(vehicles);
        }

        public Task<VehicleResponseDto> GetByIdAsync(int id)
        {
            var vehicle = _applicationDbContext.Vehicles.Find(id);
            if (vehicle == null)
            {
                throw new KeyNotFoundException($"Vehicle with ID {id} not found.");
            }
            return Task.FromResult(new VehicleResponseDto
            {
                Id = vehicle.Id,
                LicensePlate = vehicle.LicensePlate,
                Model = vehicle.Model,
                Type = vehicle.Type,
                EmissionFactor = vehicle.EmissionFactor
            });
        }

        public Task<IEnumerable<VehicleResponseDto>> GetByTypeAsync(VehicleType type)
        {
            var vehicles = _applicationDbContext.Vehicles
                .Where(v => v.Type == type)
                .Select(v => new VehicleResponseDto
                {
                    Id = v.Id,
                    LicensePlate = v.LicensePlate,
                    Model = v.Model,
                    Type = v.Type,
                    EmissionFactor = v.EmissionFactor
                }).ToList();
            return Task.FromResult<IEnumerable<VehicleResponseDto>>(vehicles);
        }

        public Task<VehicleResponseDto> UpdateAsync(int id, VehicleRequestDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Vehicle request DTO cannot be null.");
            }
            var vehicle = _applicationDbContext.Vehicles.Find(id);
            if (vehicle == null)
            {
                throw new KeyNotFoundException($"Vehicle with ID {id} not found.");
            }
            vehicle.LicensePlate = dto.LicensePlate;
            vehicle.Model = dto.Model;
            vehicle.Type = dto.Type;
            vehicle.EmissionFactor = dto.EmissionFactor;
            _applicationDbContext.SaveChanges();
            return Task.FromResult(new VehicleResponseDto
            {
                Id = vehicle.Id,
                LicensePlate = vehicle.LicensePlate,
                Model = vehicle.Model,
                Type = vehicle.Type,
                EmissionFactor = vehicle.EmissionFactor
            });
        }
    }
}

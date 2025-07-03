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
    public class MaintenanceLogService : IMaintenanceLogService
    {

        private readonly ApplicationDbContext _applicationDbContext;

        public MaintenanceLogService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public Task<MaintenanceLogResponseDto> AddAsync(MaintenanceLogRequestDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Maintenance log request DTO cannot be null.");
            }
            var maintenanceLog = new MaintenanceLog
            {
                VehicleId = dto.VehicleId,
                MaintenanceDate = dto.MaintenanceDate,
                Description = dto.Description,
                NewEmissionFactor = dto.NewEmissionFactor,
            };
            _applicationDbContext.MaintenanceLogs.Add(maintenanceLog);
            _applicationDbContext.SaveChanges();
            return Task.FromResult(new MaintenanceLogResponseDto
            {
                Id = maintenanceLog.Id,
                VehicleId = maintenanceLog.VehicleId,
                MaintenanceDate = maintenanceLog.MaintenanceDate,
                Description = maintenanceLog.Description,
                NewEmissionFactor = maintenanceLog.NewEmissionFactor
            });
        }

        public Task DeleteAsync(int id)
        {
            var maintenanceLog = _applicationDbContext.MaintenanceLogs.Find(id);
            if (maintenanceLog == null)
            {
                throw new KeyNotFoundException($"Maintenance log with ID {id} not found.");
            }
            _applicationDbContext.MaintenanceLogs.Remove(maintenanceLog);
            _applicationDbContext.SaveChanges();
            return Task.CompletedTask;
        }

        public Task<IEnumerable<MaintenanceLogResponseDto>> GetAllAsync()
        {
            var logs = _applicationDbContext.MaintenanceLogs
                .Select(log => new MaintenanceLogResponseDto
                {
                    Id = log.Id,
                    VehicleId = log.VehicleId,
                    MaintenanceDate = log.MaintenanceDate,
                    Description = log.Description,
                    NewEmissionFactor = log.NewEmissionFactor
                }).ToList();
            return Task.FromResult<IEnumerable<MaintenanceLogResponseDto>>(logs);
        }

        public Task<MaintenanceLogResponseDto> GetByIdAsync(int id)
        {
            var log = _applicationDbContext.MaintenanceLogs
                .Where(l => l.Id == id)
                .Select(l => new MaintenanceLogResponseDto
                {
                    Id = l.Id,
                    VehicleId = l.VehicleId,
                    MaintenanceDate = l.MaintenanceDate,
                    Description = l.Description,
                    NewEmissionFactor = l.NewEmissionFactor
                }).FirstOrDefault();
            if (log == null)
            {
                throw new KeyNotFoundException($"Maintenance log with ID {id} not found.");
            }
            return Task.FromResult(log);
        }

        public Task<IEnumerable<MaintenanceLogResponseDto>> GetLogsByVehicleAsync(int vehicleId)
        {
            var logs = _applicationDbContext.MaintenanceLogs
                .Where(l => l.VehicleId == vehicleId)
                .Select(l => new MaintenanceLogResponseDto
                {
                    Id = l.Id,
                    VehicleId = l.VehicleId,
                    MaintenanceDate = l.MaintenanceDate,
                    Description = l.Description,
                    NewEmissionFactor = l.NewEmissionFactor
                }).ToList();
            return Task.FromResult<IEnumerable<MaintenanceLogResponseDto>>(logs);
        }

        public Task<MaintenanceLogResponseDto> UpdateAsync(int id, MaintenanceLogRequestDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Maintenance log request DTO cannot be null.");
            }
            var maintenanceLog = _applicationDbContext.MaintenanceLogs.Find(id);
            if (maintenanceLog == null)
            {
                throw new KeyNotFoundException($"Maintenance log with ID {id} not found.");
            }
            maintenanceLog.VehicleId = dto.VehicleId;
            maintenanceLog.MaintenanceDate = dto.MaintenanceDate;
            maintenanceLog.Description = dto.Description;
            maintenanceLog.NewEmissionFactor = dto.NewEmissionFactor;
            _applicationDbContext.SaveChanges();
            return Task.FromResult(new MaintenanceLogResponseDto
            {
                Id = maintenanceLog.Id,
                VehicleId = maintenanceLog.VehicleId,
                MaintenanceDate = maintenanceLog.MaintenanceDate,
                Description = maintenanceLog.Description,
                NewEmissionFactor = maintenanceLog.NewEmissionFactor
            });
        }
    }
}

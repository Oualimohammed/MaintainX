using Pri.Ek2.Core.Data;
using Pri.Ek2.Core.Dtos.RequestDtos;
using Pri.Ek2.Core.Dtos.ResponseDtos;
using Pri.Ek2.Core.Entities;
using Pri.Ek2.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<MaintenanceLogResponseDto> AddAsync(MaintenanceLogRequestDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "Maintenance log request DTO cannot be null.");

            var maintenanceLog = new MaintenanceLog
            {
                VehicleId = dto.VehicleId,
                MaintenanceDate = dto.MaintenanceDate,
                Description = dto.Description,
                NewEmissionFactor = dto.NewEmissionFactor,
                MileageAtMaintenance = dto.MileageAtMaintenance,
                AttachmentPaths = dto.AttachmentPaths ?? new List<string>()
            };

            await _applicationDbContext.MaintenanceLogs.AddAsync(maintenanceLog);
            await _applicationDbContext.SaveChangesAsync();

            var schedule = await _applicationDbContext.MaintenanceSchedules
                .Where(s => s.VehicleId == dto.VehicleId && s.Status == "Pending")
                .OrderBy(s => s.NextMaintenanceDueDate)
                .FirstOrDefaultAsync();

            if (schedule != null && Math.Abs((schedule.NextMaintenanceDueDate - dto.MaintenanceDate).TotalDays) <= 14)
            {
                schedule.Status = "Completed";
                schedule.LastMaintenanceDate = dto.MaintenanceDate;
                schedule.MileageAtLastMaintenance = dto.MileageAtMaintenance ?? schedule.MileageAtLastMaintenance;
                schedule.NextMaintenanceDueDate = dto.MaintenanceDate.AddMonths(6);
                schedule.NextMaintenanceMileage = schedule.MileageAtLastMaintenance + 10000;

                await _applicationDbContext.SaveChangesAsync();
            }

            return new MaintenanceLogResponseDto
            {
                Id = maintenanceLog.Id,
                VehicleId = maintenanceLog.VehicleId,
                MaintenanceDate = maintenanceLog.MaintenanceDate,
                Description = maintenanceLog.Description,
                NewEmissionFactor = maintenanceLog.NewEmissionFactor,
                AttachmentPaths = maintenanceLog.AttachmentPaths
            };
        }

        public async Task DeleteAsync(int id)
        {
            var maintenanceLog = await _applicationDbContext.MaintenanceLogs.FindAsync(id);
            if (maintenanceLog == null)
                throw new KeyNotFoundException($"Maintenance log with ID {id} not found.");

            _applicationDbContext.MaintenanceLogs.Remove(maintenanceLog);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<MaintenanceLogResponseDto>> GetAllAsync()
        {
            var logs = await _applicationDbContext.MaintenanceLogs
                .Select(log => new MaintenanceLogResponseDto
                {
                    Id = log.Id,
                    VehicleId = log.VehicleId,
                    MaintenanceDate = log.MaintenanceDate,
                    Description = log.Description,
                    NewEmissionFactor = log.NewEmissionFactor,
                    AttachmentPaths = log.AttachmentPaths ?? new List<string>()
                })
                .ToListAsync();

            return logs;
        }

        public async Task<MaintenanceLogResponseDto> GetByIdAsync(int id)
        {
            var log = await _applicationDbContext.MaintenanceLogs
                .Where(l => l.Id == id)
                .Select(l => new MaintenanceLogResponseDto
                {
                    Id = l.Id,
                    VehicleId = l.VehicleId,
                    MaintenanceDate = l.MaintenanceDate,
                    Description = l.Description,
                    NewEmissionFactor = l.NewEmissionFactor,
                    AttachmentPaths = l.AttachmentPaths ?? new List<string>()
                })
                .FirstOrDefaultAsync();

            if (log == null)
                throw new KeyNotFoundException($"Maintenance log with ID {id} not found.");

            return log;
        }

        public async Task<IEnumerable<MaintenanceLogResponseDto>> GetLogsByVehicleAsync(int vehicleId)
        {
            var logs = await _applicationDbContext.MaintenanceLogs
                .Where(l => l.VehicleId == vehicleId)
                .Select(l => new MaintenanceLogResponseDto
                {
                    Id = l.Id,
                    VehicleId = l.VehicleId,
                    MaintenanceDate = l.MaintenanceDate,
                    Description = l.Description,
                    NewEmissionFactor = l.NewEmissionFactor,
                    AttachmentPaths = l.AttachmentPaths ?? new List<string>()
                })
                .ToListAsync();

            return logs;
        }

        public async Task<MaintenanceLogResponseDto> UpdateAsync(int id, MaintenanceLogRequestDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "Maintenance log request DTO cannot be null.");

            var maintenanceLog = await _applicationDbContext.MaintenanceLogs.FindAsync(id);
            if (maintenanceLog == null)
                throw new KeyNotFoundException($"Maintenance log with ID {id} not found.");

            maintenanceLog.VehicleId = dto.VehicleId;
            maintenanceLog.MaintenanceDate = dto.MaintenanceDate;
            maintenanceLog.Description = dto.Description;
            maintenanceLog.NewEmissionFactor = dto.NewEmissionFactor;
            maintenanceLog.AttachmentPaths = dto.AttachmentPaths ?? new List<string>();

            await _applicationDbContext.SaveChangesAsync();

            return new MaintenanceLogResponseDto
            {
                Id = maintenanceLog.Id,
                VehicleId = maintenanceLog.VehicleId,
                MaintenanceDate = maintenanceLog.MaintenanceDate,
                Description = maintenanceLog.Description,
                NewEmissionFactor = maintenanceLog.NewEmissionFactor,
                AttachmentPaths = maintenanceLog.AttachmentPaths
            };
        }

        public async Task AddAttachmentAsync(int logId, string filePath)
        {
            var maintenanceLog = await _applicationDbContext.MaintenanceLogs.FindAsync(logId);
            if (maintenanceLog == null)
                throw new KeyNotFoundException($"Maintenance log with ID {logId} not found.");

            if (maintenanceLog.AttachmentPaths == null)
                maintenanceLog.AttachmentPaths = new List<string>();

            maintenanceLog.AttachmentPaths.Add(filePath);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}

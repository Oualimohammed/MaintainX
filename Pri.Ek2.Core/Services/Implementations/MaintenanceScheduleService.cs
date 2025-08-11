using Microsoft.EntityFrameworkCore;
using Pri.Ek2.Core.Data;
using Pri.Ek2.Core.Dtos.RequestDtos;
using Pri.Ek2.Core.Dtos.ResponseDtos;
using Pri.Ek2.Core.Entities;
using Pri.Ek2.Core.Services.Interfaces;

namespace Pri.Ek2.Core.Services.Implementations
{
    public class MaintenanceScheduleService : IMaintenanceScheduleService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public MaintenanceScheduleService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<MaintenanceScheduleResponseDto>> GetAllAsync()
        {
            var schedules = await _applicationDbContext.MaintenanceSchedules.ToListAsync();
            return schedules.Select(MapToResponseDto);
        }

        public async Task<MaintenanceScheduleResponseDto> GetByIdAsync(int id)
        {
            var schedule = await _applicationDbContext.MaintenanceSchedules.FindAsync(id);
            if (schedule == null)
                throw new KeyNotFoundException("Onderhoudsschema niet gevonden.");

            return MapToResponseDto(schedule);
        }

        public async Task<IEnumerable<MaintenanceScheduleResponseDto>> GetByVehicleAsync(int vehicleId)
        {
            var schedules = await _applicationDbContext.MaintenanceSchedules
                .Where(s => s.VehicleId == vehicleId)
                .ToListAsync();

            return schedules.Select(MapToResponseDto);
        }

        public async Task<MaintenanceScheduleResponseDto> AddAsync(MaintenanceScheduleRequestDto dto)
        {
            var entity = new MaintenanceSchedule
            {
                VehicleId = dto.VehicleId,
                LastMaintenanceDate = dto.LastMaintenanceDate,
                NextMaintenanceDueDate = dto.NextMaintenanceDueDate,
                MileageAtLastMaintenance = dto.MileageAtLastMaintenance,
                NextMaintenanceMileage = dto.NextMaintenanceMileage,
                Status = dto.Status,
                Notes = dto.Notes
            };

            _applicationDbContext.MaintenanceSchedules.Add(entity);

            var geplandeLog = new MaintenanceLog
            {
                VehicleId = dto.VehicleId,
                MaintenanceDate = dto.NextMaintenanceDueDate,
                Description = "Gepland onderhoud",
                NewEmissionFactor = null
            };
            _applicationDbContext.MaintenanceLogs.Add(geplandeLog);
            await _applicationDbContext.SaveChangesAsync();

            return MapToResponseDto(entity);

        }

        public async Task<MaintenanceScheduleResponseDto> UpdateAsync(int id, MaintenanceScheduleRequestDto dto)
        {
            var entity = await _applicationDbContext.MaintenanceSchedules.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Onderhoudsschema niet gevonden.");

            entity.VehicleId = dto.VehicleId;
            entity.LastMaintenanceDate = dto.LastMaintenanceDate;
            entity.NextMaintenanceDueDate = dto.NextMaintenanceDueDate;
            entity.MileageAtLastMaintenance = dto.MileageAtLastMaintenance;
            entity.NextMaintenanceMileage = dto.NextMaintenanceMileage;
            entity.Status = dto.Status;
            entity.Notes = dto.Notes;

            await _applicationDbContext.SaveChangesAsync();

            return MapToResponseDto(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _applicationDbContext.MaintenanceSchedules.FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Onderhoudsschema niet gevonden.");

            _applicationDbContext.MaintenanceSchedules.Remove(entity);
            await _applicationDbContext.SaveChangesAsync();
        }

        private static MaintenanceScheduleResponseDto MapToResponseDto(MaintenanceSchedule entity)
        {
            return new MaintenanceScheduleResponseDto
            {
                Id = entity.Id,
                VehicleId = entity.VehicleId,
                AssignedMechanicId = entity.AssignedMechanicId, // Voeg deze toe
                LastMaintenanceDate = entity.LastMaintenanceDate,
                NextMaintenanceDueDate = entity.NextMaintenanceDueDate,
                MileageAtLastMaintenance = entity.MileageAtLastMaintenance,
                NextMaintenanceMileage = entity.NextMaintenanceMileage,
                Status = entity.Status,
                Notes = entity.Notes
            };
        }

        public async Task<IEnumerable<MaintenanceScheduleResponseDto>> GetSchedulesByMechanicAsync(string mechanicId)
        {
            var schedules = await _applicationDbContext.MaintenanceSchedules
                .Where(s => s.AssignedMechanicId == mechanicId)
                .ToListAsync();

            return schedules.Select(MapToResponseDto);
        }
    }
}

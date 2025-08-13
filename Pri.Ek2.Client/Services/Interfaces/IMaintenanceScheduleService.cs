using Pri.Ek2.Client.Dtos.RequestDtos;
using Pri.Ek2.Client.Dtos.ResponseDtos;

namespace Pri.Ek2.Client.Services.Interfaces
{
    public interface IMaintenanceScheduleService
    {
        Task<IEnumerable<MaintenanceScheduleResponseDto>> GetAllAsync();
        Task<MaintenanceScheduleResponseDto> GetByIdAsync(int id);
        Task<IEnumerable<MaintenanceScheduleResponseDto>> GetByVehicleAsync(int vehicleId);
        Task<MaintenanceScheduleResponseDto> AddAsync(MaintenanceScheduleRequestDto dto);
        Task<MaintenanceScheduleResponseDto> UpdateAsync(int id, MaintenanceScheduleRequestDto dto);
        Task DeleteAsync(int id);
        Task<IEnumerable<MaintenanceScheduleResponseDto>> GetSchedulesByMechanicAsync(string mechanicId);
        Task<IEnumerable<MaintenanceScheduleResponseDto>> GetAssignedSchedulesAsync(string userId);


    }
}

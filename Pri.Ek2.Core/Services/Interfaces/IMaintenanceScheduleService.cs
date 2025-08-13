using Pri.Ek2.Core.Dtos.RequestDtos;
using Pri.Ek2.Core.Dtos.ResponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ek2.Core.Services.Interfaces
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
    }
}

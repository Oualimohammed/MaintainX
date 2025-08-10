using Pri.Ek2.Core.Dtos.RequestDtos;
using Pri.Ek2.Core.Dtos.ResponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ek2.Core.Services.Interfaces
{
    public interface IMaintenanceLogService
    {
        Task<IEnumerable<MaintenanceLogResponseDto>> GetAllAsync();
        Task<MaintenanceLogResponseDto> GetByIdAsync(int id);
        Task<MaintenanceLogResponseDto> AddAsync(MaintenanceLogRequestDto dto);
        Task<MaintenanceLogResponseDto> UpdateAsync(int id, MaintenanceLogRequestDto dto);
        Task DeleteAsync(int id);
        // Optioneel:
        Task<IEnumerable<MaintenanceLogResponseDto>> GetLogsByVehicleAsync(int vehicleId);
        Task AddAttachmentAsync(int logId, string filePath); 
    }
}

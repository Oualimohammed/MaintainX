using Pri.Ek2.Client.Dtos.RequestDtos;
using Pri.Ek2.Client.Dtos.ResponseDtos;

namespace Pri.Ek2.Client.Services.Interfaces
{
    public interface IMaintenanceLogService
    {
        Task<IEnumerable<MaintenanceLogResponseDto>> GetAllAsync();
        Task<MaintenanceLogResponseDto> GetByIdAsync(int id);
        Task<IEnumerable<MaintenanceLogResponseDto>> GetLogsByVehicleAsync(int vehicleId);
        Task<MaintenanceLogResponseDto> AddAsync(MaintenanceLogRequestDto dto);
        Task<MaintenanceLogResponseDto> UpdateAsync(int id, MaintenanceLogRequestDto dto);
        Task DeleteAsync(int id);
        Task<string> UploadAttachmentAsync(int logId, MultipartFormDataContent content);
    }
}

using Pri.Ek2.Client.Dtos.RequestDtos;
using Pri.Ek2.Client.Dtos.ResponseDtos;

namespace Pri.Ek2.Client.Services.Interfaces
{
    public interface IVehicleService
    {
        Task<IEnumerable<VehicleResponseDto>> GetAllAsync();
        Task<VehicleResponseDto> GetByIdAsync(int id);
        Task<IEnumerable<VehicleResponseDto>> GetByTypeAsync(string type);
        Task<VehicleResponseDto> AddAsync(VehicleRequestDto dto);
        Task<VehicleResponseDto> UpdateAsync(int id, VehicleRequestDto dto);
        Task DeleteAsync(int id);
    }
}

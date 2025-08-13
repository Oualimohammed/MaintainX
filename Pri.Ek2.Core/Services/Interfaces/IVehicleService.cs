using Pri.Ek2.Core.Dtos.RequestDtos;
using Pri.Ek2.Core.Dtos.ResponseDtos;
using Pri.Ek2.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ek2.Core.Services.Interfaces
{
    public interface IVehicleService 
    {
        Task<IEnumerable<VehicleResponseDto>> GetAllAsync();
        Task<VehicleResponseDto> GetByIdAsync(int id);
        Task<VehicleResponseDto> AddAsync(VehicleRequestDto dto);
        Task<VehicleResponseDto> UpdateAsync(int id, VehicleRequestDto dto);
        Task DeleteAsync(int id);
        // Optionele methods:
        Task<IEnumerable<VehicleResponseDto>> GetByTypeAsync(VehicleType type);
    }
}

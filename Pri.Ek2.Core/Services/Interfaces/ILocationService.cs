using Pri.Ek2.Core.Dtos.RequestDtos;
using Pri.Ek2.Core.Dtos.ResponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ek2.Core.Services.Interfaces
{
    public interface ILocationService
    {
        Task<IEnumerable<LocationResponseDto>> GetAllAsync();
        Task<LocationResponseDto> GetByIdAsync(int id);
        Task<LocationResponseDto> AddAsync(LocationRequestDto dto);
        Task<LocationResponseDto> UpdateAsync(int id, LocationRequestDto dto);
        Task DeleteAsync(int id);
        // Optioneel:
        Task<IEnumerable<LocationResponseDto>> GetLocationsByCriteriaAsync(string criteria);
    }
}

using Pri.Ek2.Core.Dtos.RequestDtos;
using Pri.Ek2.Core.Dtos.ResponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ek2.Core.Services.Interfaces
{
    public interface IEmissionGoalService
    {
        Task<IEnumerable<EmissionGoalResponseDto>> GetAllAsync();
        Task<EmissionGoalResponseDto> GetByIdAsync(int id);
        Task<EmissionGoalResponseDto> AddAsync(EmissionGoalRequestDto dto);
        Task<EmissionGoalResponseDto> UpdateAsync(int id, EmissionGoalRequestDto dto);
        Task DeleteAsync(int id);
        // Optioneel:
        Task<IEnumerable<EmissionGoalResponseDto>> GetGoalsByCriteriaAsync(string criteria);
    }
}

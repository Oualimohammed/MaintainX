using Pri.Ek2.Core.Dtos.RequestDtos;
using Pri.Ek2.Core.Dtos.ResponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ek2.Core.Services.Interfaces
{
    public interface IRewardService
    {
        Task<IEnumerable<RewardResponseDto>> GetAllAsync();
        Task<RewardResponseDto> GetByIdAsync(int id);
        Task<RewardResponseDto> AddAsync(RewardRequestDto dto);
        Task<RewardResponseDto> UpdateAsync(int id, RewardRequestDto dto);
        Task DeleteAsync(int id);
        // Optioneel:
        Task<IEnumerable<RewardResponseDto>> GetRewardsByCriteriaAsync(string criteria);
    }
}

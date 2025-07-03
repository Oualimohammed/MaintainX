using Pri.Ek2.Core.Dtos.RequestDtos;
using Pri.Ek2.Core.Dtos.ResponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ek2.Core.Services.Interfaces
{
    public interface IUserProfileService
    {
        Task<UserProfileResponseDto?> GetUserProfileAsync(string userId);
        Task<IEnumerable<UserProfileResponseDto>> GetAllUserProfilesAsync();
        Task<bool> DeleteUserProfileAsync(string userId);

        Task<UserProfileResponseDto> CreateUserProfileAsync(string userId, UserProfileRequestDto dto);
        Task<UserProfileResponseDto> UpdateUserProfileAsync(string userId, UserProfileRequestDto dto);
    }
}

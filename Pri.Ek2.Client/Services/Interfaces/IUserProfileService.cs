using Pri.Ek2.Client.Dtos.RequestDtos;
using Pri.Ek2.Client.Dtos.ResponseDtos;

namespace Pri.Ek2.Client.Services.Interfaces
{
    public interface IUserProfileService
    {
        Task<IEnumerable<UserProfileResponseDto>> GetAllUserProfilesAsync();
        Task<UserProfileResponseDto> GetUserProfileAsync(string userId);
        Task<UserProfileResponseDto> CreateUserProfileAsync(string userId, UserProfileRequestDto dto);
        Task<UserProfileResponseDto> UpdateUserProfileAsync(string userId, UserProfileRequestDto dto);
        Task DeleteUserProfileAsync(string userId);
    }
}

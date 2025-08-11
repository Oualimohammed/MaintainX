using Pri.Ek2.Client.Dtos.RequestDtos;
using Pri.Ek2.Client.Dtos.ResponseDtos;

namespace Pri.Ek2.Client.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsersAsync();
        Task ResetPasswordAsync(ResetPasswordRequestDto dto);
    }
}


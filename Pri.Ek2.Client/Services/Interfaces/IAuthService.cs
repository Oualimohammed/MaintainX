using Pri.Ek2.Client.Dtos.AuthDtos;

namespace Pri.Ek2.Client.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterRequestDto registerRequestDto);
        Task<AuthResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
    }
}

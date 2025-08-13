using Pri.Ek2.Client.Dtos.RequestDtos;
using Pri.Ek2.Client.Dtos.ResponseDtos;
using Pri.Ek2.Client.Services.Interfaces;
using System.Net.Http.Json;

namespace Pri.Ek2.Client.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly HttpClient _http;

        public UserService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            return await _http.GetFromJsonAsync<List<UserDto>>("api/users");
        }

        public async Task ResetPasswordAsync(ResetPasswordRequestDto dto)
        {
            await _http.PostAsJsonAsync("api/users/reset-password", dto);
        }
    }
}

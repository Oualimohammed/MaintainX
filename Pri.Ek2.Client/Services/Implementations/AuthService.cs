using Pri.Ek2.Client.Dtos.AuthDtos;
using Pri.Ek2.Client.Services.Interfaces;
using System.Net.Http.Json;

namespace Pri.Ek2.Client.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginRequestDto);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Login mislukt: {error}");
            }

            return await response.Content.ReadFromJsonAsync<AuthResponseDto>();
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto registerRequestDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/register", registerRequestDto);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Registratie mislukt: {error}");
            }
            return await response.Content.ReadFromJsonAsync<AuthResponseDto>();
        }
    }
}

using Pri.Ek2.Client.Dtos.AuthDtos;
using Pri.Ek2.Client.Services.Interfaces;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components.Authorization;
using Pri.Ek2.Client.Services.Authentication;

namespace Pri.Ek2.Client.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthService(HttpClient httpClient, IJSRuntime jsRuntime, AuthenticationStateProvider authStateProvider)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
            _authStateProvider = authStateProvider;
        }

        public async Task InitializeAsync()
        {
            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
            if (!string.IsNullOrWhiteSpace(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginRequestDto);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Login mislukt: {error}");
            }

            var authResponse = await response.Content.ReadFromJsonAsync<AuthResponseDto>();

            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "authToken", authResponse.Token);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResponse.Token);

            // ✅ Informeer AuthenticationStateProvider
            if (_authStateProvider is CustomAuthStateProvider customAuth)
            {
                customAuth.NotifyUserAuthentication(authResponse.Token);
            }

            return authResponse;
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

        public async Task LogoutAsync()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authToken");
            _httpClient.DefaultRequestHeaders.Authorization = null;

            // ✅ Informeer AuthenticationStateProvider
            if (_authStateProvider is CustomAuthStateProvider customAuth)
            {
                customAuth.NotifyUserLogout();
            }
        }
    }
}

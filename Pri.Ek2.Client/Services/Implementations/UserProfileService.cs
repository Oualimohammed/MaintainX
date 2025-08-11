using Pri.Ek2.Client.Dtos.RequestDtos;
using Pri.Ek2.Client.Dtos.ResponseDtos;
using Pri.Ek2.Client.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;


namespace Pri.Ek2.Client.Services.Implementations
{
    public class UserProfileService : IUserProfileService
    {
        private readonly HttpClient _httpClient;

        public UserProfileService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<UserProfileResponseDto>> GetAllUserProfilesAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<UserProfileResponseDto>>("api/userprofile");
        }

        public async Task<UserProfileResponseDto?> GetUserProfileAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"api/userprofile/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                // Optioneel: loggen wat er fout ging
                var errorText = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Fout bij ophalen profiel: {errorText}");
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(content))
            {
                return null; // Geen profiel gevonden
            }

            return JsonSerializer.Deserialize<UserProfileResponseDto>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }


        public async Task<UserProfileResponseDto> CreateUserProfileAsync(string userId, UserProfileRequestDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/userprofile", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<UserProfileResponseDto>();
        }

        public async Task<UserProfileResponseDto> UpdateUserProfileAsync(string userId, UserProfileRequestDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync("api/userprofile", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<UserProfileResponseDto>();
        }

        public async Task DeleteUserProfileAsync(string userId)
        {
            var response = await _httpClient.DeleteAsync($"api/userprofile/{userId}");
            response.EnsureSuccessStatusCode();
        }
    }
}

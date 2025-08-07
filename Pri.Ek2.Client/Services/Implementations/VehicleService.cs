using Pri.Ek2.Client.Dtos.RequestDtos;
using Pri.Ek2.Client.Dtos.ResponseDtos;
using Pri.Ek2.Client.Services.Interfaces;
using System.Net.Http.Json;

namespace Pri.Ek2.Client.Services.Implementations
{
    public class VehicleService : IVehicleService
    {
        private readonly HttpClient _httpClient;

        public VehicleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<VehicleResponseDto>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<VehicleResponseDto>>("api/vehicles");
        }

        public async Task<VehicleResponseDto> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<VehicleResponseDto>($"api/vehicles/{id}");
        }

        public async Task<IEnumerable<VehicleResponseDto>> GetByTypeAsync(string type)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<VehicleResponseDto>>($"api/vehicles/type/{type}");
        }

        public async Task<VehicleResponseDto> AddAsync(VehicleRequestDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/vehicles", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<VehicleResponseDto>();
        }

        public async Task<VehicleResponseDto> UpdateAsync(int id, VehicleRequestDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/vehicles/{id}", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<VehicleResponseDto>();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/vehicles/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}

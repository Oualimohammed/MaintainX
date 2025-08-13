using Pri.Ek2.Client.Dtos.RequestDtos;
using Pri.Ek2.Client.Dtos.ResponseDtos;
using Pri.Ek2.Client.Services.Interfaces;
using System.Net.Http.Json;

namespace Pri.Ek2.Client.Services.Implementations
{
    public class MaintenanceLogService : IMaintenanceLogService
    {

        private readonly HttpClient _httpClient;

        public MaintenanceLogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<MaintenanceLogResponseDto>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/maintenanceLog");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<MaintenanceLogResponseDto>>();
        }

        public async Task<MaintenanceLogResponseDto> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/maintenanceLog/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<MaintenanceLogResponseDto>();
        }

        public async Task<IEnumerable<MaintenanceLogResponseDto>> GetLogsByVehicleAsync(int vehicleId)
        {
            var response = await _httpClient.GetAsync($"api/maintenanceLog/vehicle/{vehicleId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<MaintenanceLogResponseDto>>();
        }

        public async Task<MaintenanceLogResponseDto> AddAsync(MaintenanceLogRequestDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/maintenanceLog", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<MaintenanceLogResponseDto>();
        }

        public async Task<MaintenanceLogResponseDto> UpdateAsync(int id, MaintenanceLogRequestDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/maintenanceLog/{id}", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<MaintenanceLogResponseDto>();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/maintenanceLog/{id}");
            response.EnsureSuccessStatusCode();

        }

        public async Task<string> UploadAttachmentAsync(int logId, MultipartFormDataContent content)
        {
            var response = await _httpClient.PostAsync($"api/maintenanceLog/{logId}/attachments", content);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            return result["url"]; 
        }

    }
}

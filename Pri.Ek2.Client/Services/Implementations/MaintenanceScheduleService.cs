using Pri.Ek2.Client.Dtos.RequestDtos;
using Pri.Ek2.Client.Dtos.ResponseDtos;
using Pri.Ek2.Client.Services.Interfaces;
using System.Net.Http.Json;

namespace Pri.Ek2.Client.Services.Implementations
{
    public class MaintenanceScheduleService : IMaintenanceScheduleService
    {
        private readonly HttpClient _httpClient;

        public MaintenanceScheduleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<MaintenanceScheduleResponseDto>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<MaintenanceScheduleResponseDto>>("api/maintenanceSchedule");
        }

        public async Task<MaintenanceScheduleResponseDto> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<MaintenanceScheduleResponseDto>($"api/maintenanceSchedule/{id}");
        }

        public async Task<IEnumerable<MaintenanceScheduleResponseDto>> GetByVehicleAsync(int vehicleId)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<MaintenanceScheduleResponseDto>>($"api/maintenanceSchedule/vehicle/{vehicleId}");
        }

        public async Task<MaintenanceScheduleResponseDto> AddAsync(MaintenanceScheduleRequestDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/maintenanceSchedule", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<MaintenanceScheduleResponseDto>();
        }

        public async Task<MaintenanceScheduleResponseDto> UpdateAsync(int id, MaintenanceScheduleRequestDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/maintenanceSchedule/{id}", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<MaintenanceScheduleResponseDto>();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/maintenanceSchedule/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<MaintenanceScheduleResponseDto>> GetSchedulesByMechanicAsync(string mechanicId)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<MaintenanceScheduleResponseDto>>(
                $"api/maintenanceSchedule/mechanic/{mechanicId}");
        }

        public async Task<IEnumerable<MaintenanceScheduleResponseDto>> GetAssignedSchedulesAsync(string userId)
        {
            var allSchedules = await GetAllAsync();
            return allSchedules.Where(s => s.AssignedMechanicId == userId);
        }
    }
}

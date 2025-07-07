using Pri.Ek2.Core.Dtos.RequestDtos;
using Pri.Ek2.Core.Dtos.ResponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ek2.Core.Services.Interfaces
{
    public interface ITransportRouteService
    {
        Task<IEnumerable<TransportRouteResponseDto>> GetAllAsync();
        Task<TransportRouteResponseDto> GetByIdAsync(int id);
        Task<TransportRouteResponseDto> AddAsync(TransportRouteRequestDto dto);
        Task<TransportRouteResponseDto> UpdateAsync(int id, TransportRouteRequestDto dto);
        Task DeleteAsync(int id);
        // Optioneel:
        Task<IEnumerable<TransportRouteResponseDto>> GetRoutesByVehicleAsync(int vehicleId);
        Task UpdateProofPathAsync(int id, string proofPath);
    }
}

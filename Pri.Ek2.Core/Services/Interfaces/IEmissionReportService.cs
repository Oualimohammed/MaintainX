using Pri.Ek2.Core.Dtos.RequestDtos;
using Pri.Ek2.Core.Dtos.ResponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ek2.Core.Services.Interfaces
{
    public interface IEmissionReportService
    {
        Task<IEnumerable<EmissionReportResponseDto>> GetAllAsync();
        Task<EmissionReportResponseDto> GetByIdAsync(int id);
        Task<EmissionReportResponseDto> AddAsync(EmissionReportRequestDto dto);
        Task<EmissionReportResponseDto> UpdateAsync(int id, EmissionReportRequestDto dto);
        Task DeleteAsync(int id);
        // Optioneel:
        Task<IEnumerable<EmissionReportResponseDto>> GetReportsByCriteriaAsync(string criteria);
    }
}

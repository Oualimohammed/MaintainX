using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Hosting;
using Pri.Ek2.Core.Data;
using Pri.Ek2.Core.Dtos.RequestDtos;
using Pri.Ek2.Core.Dtos.ResponseDtos;
using Pri.Ek2.Core.Services.Interfaces;

namespace Pri.Ek2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Mechanic,Admin")]
    public class MaintenanceLogController : ControllerBase
    {
        private readonly IMaintenanceLogService _maintenanceService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public MaintenanceLogController(IMaintenanceLogService maintenanceService, IWebHostEnvironment hostEnvironment)
        {
            _maintenanceService = maintenanceService;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<MaintenanceLogResponseDto>>> GetAll()
        {
            var logs = await _maintenanceService.GetAllAsync();
            return Ok(logs);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<MaintenanceLogResponseDto>> GetById(int id)
        {
            try
            {
                var log = await _maintenanceService.GetByIdAsync(id);
                return Ok(log);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Onderhoudslog {id} niet gevonden.");
            }
        }

        [HttpGet("vehicle/{vehicleId}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<MaintenanceLogResponseDto>>> GetByVehicle(int vehicleId)
        {
            var logs = await _maintenanceService.GetLogsByVehicleAsync(vehicleId);
            return Ok(logs);
        }
      

    }
}

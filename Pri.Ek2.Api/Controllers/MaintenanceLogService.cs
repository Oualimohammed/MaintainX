using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pri.Ek2.Core.Dtos.RequestDtos;
using Pri.Ek2.Core.Dtos.ResponseDtos;
using Pri.Ek2.Core.Services.Interfaces;

namespace Pri.Ek2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Mechanic,Admin")]
    public class MaintenanceController : ControllerBase
    {
        private readonly IMaintenanceLogService _maintenanceService;

        public MaintenanceController(IMaintenanceLogService maintenanceService)
        {
            _maintenanceService = maintenanceService;
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
    }
}

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
    public class MaintenanceScheduleController : ControllerBase
    {
        private readonly IMaintenanceScheduleService _maintenanceScheduleService;

        public MaintenanceScheduleController(IMaintenanceScheduleService maintenanceScheduleService)
        {
            _maintenanceScheduleService = maintenanceScheduleService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<MaintenanceScheduleResponseDto>>> GetAll()
        {
            var schedules = await _maintenanceScheduleService.GetAllAsync();
            return Ok(schedules);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<MaintenanceScheduleResponseDto>> GetById(int id)
        {
            try
            {
                var schedule = await _maintenanceScheduleService.GetByIdAsync(id);
                return Ok(schedule);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Onderhoudsschema {id} niet gevonden.");
            }
        }

        [HttpGet("vehicle/{vehicleId}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<MaintenanceScheduleResponseDto>>> GetByVehicle(int vehicleId)
        {
            var schedules = await _maintenanceScheduleService.GetByVehicleAsync(vehicleId);
            return Ok(schedules);
        }

        [HttpPost]
        public async Task<ActionResult<MaintenanceScheduleResponseDto>> Add([FromBody] MaintenanceScheduleRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _maintenanceScheduleService.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        
    }
}


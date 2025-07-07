using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pri.Ek2.Core.Dtos.RequestDtos;
using Pri.Ek2.Core.Dtos.ResponseDtos;
using Pri.Ek2.Core.Entities;
using Pri.Ek2.Core.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pri.Ek2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehiclesController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<VehicleResponseDto>>> GetAll()
        {
            var vehicles = await _vehicleService.GetAllAsync();
            return Ok(vehicles);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<VehicleResponseDto>> GetById(int id)
        {
            try
            {
                var vehicle = await _vehicleService.GetByIdAsync(id);
                return Ok(vehicle);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Vehicle with ID {id} not found.");
            }
        }

        [HttpGet("type/{type}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<VehicleResponseDto>>> GetByType(VehicleType type)
        {
            var vehicles = await _vehicleService.GetByTypeAsync(type);
            return Ok(vehicles);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")] 
        public async Task<ActionResult<VehicleResponseDto>> Add([FromBody] VehicleRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _vehicleService.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<VehicleResponseDto>> Update(int id, VehicleRequestDto dto)
            => Ok(await _vehicleService.UpdateAsync(id, dto));

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _vehicleService.DeleteAsync(id);
            return NoContent();
        }
    }
}
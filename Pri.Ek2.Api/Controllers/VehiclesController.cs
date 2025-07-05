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
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehiclesController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleResponseDto>>> GetAll()
            => Ok(await _vehicleService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleResponseDto>> GetById(int id)
            => Ok(await _vehicleService.GetByIdAsync(id));

        [HttpGet("type/{type}")]
        public async Task<ActionResult<IEnumerable<VehicleResponseDto>>> GetByType(VehicleType type)
            => Ok(await _vehicleService.GetByTypeAsync(type));

        [HttpPost]
        [Authorize(Roles = "Admin")] 
        public async Task<ActionResult<VehicleResponseDto>> Add(VehicleRequestDto dto)
        {
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
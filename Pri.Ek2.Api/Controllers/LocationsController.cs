using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pri.Ek2.Core.Dtos.RequestDtos;
using Pri.Ek2.Core.Dtos.ResponseDtos;
using Pri.Ek2.Core.Services.Interfaces;

namespace Pri.Ek2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationsController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<LocationResponseDto>>> GetAll()
        {
            var results = await _locationService.GetAllAsync();
            return Ok(results);
        }
      
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<LocationResponseDto>> GetById(int id)
        {
            try
            {
                var result = await _locationService.GetByIdAsync(id);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Location with ID {id} not found.");
            }
        }
    }
}

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
    public class TransportRoutesController : ControllerBase
    {
        private readonly ITransportRouteService _routeService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public TransportRoutesController(
            ITransportRouteService routeService,
            IWebHostEnvironment hostEnvironment)
        {
            _routeService = routeService;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<TransportRouteResponseDto>>> GetAll()
        {
            var result = await _routeService.GetAllAsync();
            return Ok(result);
        }
        
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<TransportRouteResponseDto>> GetById(int id)
        {
            try
            {
                var route = await _routeService.GetByIdAsync(id);
                return Ok(route);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Route with ID {id} not found.");
            }
        }

    }
}

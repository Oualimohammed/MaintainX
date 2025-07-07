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

        [HttpGet("vehicle/{vehicleId}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<TransportRouteResponseDto>>> GetByVehicle(int vehicleId)
        {
            var routes = await _routeService.GetRoutesByVehicleAsync(vehicleId);
            return Ok(routes);
        }

        [HttpPost]
        public async Task<ActionResult<TransportRouteResponseDto>> Add([FromBody] TransportRouteRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _routeService.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPost("{id}/upload-proof")]
        public async Task<IActionResult> UploadProof(int id, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Geen bestand meegegeven.");

            var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "proofs");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = $"{id}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            await _routeService.UpdateProofPathAsync(id, $"/proofs/{uniqueFileName}");

            return Ok(new { filePath = $"/proofs/{uniqueFileName}" });
        }
    }
}

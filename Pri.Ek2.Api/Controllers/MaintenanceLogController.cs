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
      
        [HttpPost]
        public async Task<ActionResult<MaintenanceLogResponseDto>> Add([FromBody] MaintenanceLogRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _maintenanceService.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MaintenanceLogResponseDto>> Update(int id, [FromBody] MaintenanceLogRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updated = await _maintenanceService.UpdateAsync(id, dto);
                return Ok(updated);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Log {id} niet gevonden.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _maintenanceService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Log {id} niet gevonden.");
            }
        }

        [HttpGet("{logId}/download")]
        public async Task<IActionResult> DownloadFile(int logId, [FromQuery] string fileName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(fileName))
                    return BadRequest("Bestandsnaam ontbreekt.");

                var log = await _maintenanceService.GetByIdAsync(logId);
                if (log == null) return NotFound("Log niet gevonden");

                // Debug output
                Console.WriteLine($"📄 Opgevraagde bestandsnaam: {fileName}");
                Console.WriteLine($"📂 AttachmentPaths in DB: {string.Join(", ", log.AttachmentPaths ?? new List<string>())}");
                Console.WriteLine($"🌐 WebRootPath: {_hostEnvironment.WebRootPath}");

                var filePath = log.AttachmentPaths?
                    .FirstOrDefault(p => Path.GetFileName(p).Equals(fileName, StringComparison.OrdinalIgnoreCase));

                if (filePath == null)
                    return NotFound("Bestand niet gevonden bij deze log");

                var relativePath = filePath.Replace("\\", "/").TrimStart('/');
                var fullPath = Path.Combine(_hostEnvironment.WebRootPath, relativePath);

                Console.WriteLine($"📌 Gebouwd pad: {fullPath}");

                if (!System.IO.File.Exists(fullPath))
                    return NotFound($"Bestand niet gevonden op server: {fullPath}");

                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(fullPath, out var contentType))
                {
                    contentType = "application/octet-stream";
                }

                var fileStream = System.IO.File.OpenRead(fullPath);
                return File(fileStream, contentType, Path.GetFileName(fullPath));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Interne serverfout: {ex.Message}");
            }
        }
    }
}

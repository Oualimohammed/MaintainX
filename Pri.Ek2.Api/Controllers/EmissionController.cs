using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pri.Ek2.Core.Dtos.RequestDtos;
using Pri.Ek2.Core.Dtos.ResponseDtos;
using Pri.Ek2.Core.Services.Interfaces;
using System.Security.Claims;

namespace Pri.Ek2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmissionController : ControllerBase
    {
        private readonly IEmissionGoalService _goalService;
        private readonly IEmissionReportService _reportService;

        public EmissionController(
            IEmissionGoalService goalService,
            IEmissionReportService reportService)
        {
            _goalService = goalService;
            _reportService = reportService;
        }

        [HttpGet("goals")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<EmissionGoalResponseDto>>> GetGoals()
        {
            var results = await _goalService.GetAllAsync();
            return Ok(results);
        }

        [HttpGet("goals/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<EmissionGoalResponseDto>> GetGoalById(int id)
        {
            try
            {
                var result = await _goalService.GetByIdAsync(id);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Emissiedoel {id} niet gevonden.");
            }
        }
       
        [HttpGet("goals/user/{userId}")]
        public async Task<ActionResult<IEnumerable<EmissionGoalResponseDto>>> GetGoalsByUser(string userId)
        {
            var results = await _goalService.GetGoalsByCriteriaAsync(userId);
            return Ok(results);
        }

        [HttpPost("goals")]
        public async Task<ActionResult<EmissionGoalResponseDto>> AddGoal([FromBody] EmissionGoalRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            dto.UserId = userId; // 🔐 veilig en automatisch

            var result = await _goalService.AddAsync(dto);
            return CreatedAtAction(nameof(GetGoalById), new { id = result.Id }, result);
        }

        [HttpPut("goals/{id}")]
        public async Task<ActionResult<EmissionGoalResponseDto>> UpdateGoal(int id, [FromBody] EmissionGoalRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updated = await _goalService.UpdateAsync(id, dto);
                return Ok(updated);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Doel {id} niet gevonden.");
            }
        }

        [HttpDelete("goals/{id}")]
        public async Task<IActionResult> DeleteGoal(int id)
        {
            try
            {
                await _goalService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Doel {id} niet gevonden.");
            }
        }


        [HttpGet("reports")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<EmissionReportResponseDto>>> GetReports()
        {
            var results = await _reportService.GetAllAsync();
            return Ok(results);
        }

        [HttpGet("reports/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<EmissionReportResponseDto>> GetReportById(int id)
        {
            try
            {
                var result = await _reportService.GetByIdAsync(id);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Rapport {id} niet gevonden.");
            }
        }

        [HttpGet("reports/search")]
        public async Task<ActionResult<IEnumerable<EmissionReportResponseDto>>> SearchReports([FromQuery] string criteria)
        {
            var results = await _reportService.GetReportsByCriteriaAsync(criteria);
            return Ok(results);
        }
    }
}

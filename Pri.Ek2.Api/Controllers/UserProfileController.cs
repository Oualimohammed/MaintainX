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
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService _profileService;

        public UserProfileController(IUserProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<UserProfileResponseDto>>> GetAll()
        {
            var profiles = await _profileService.GetAllUserProfilesAsync();
            return Ok(profiles);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserProfileResponseDto>> GetByUserId(string userId)
        {
            try
            {
                var profile = await _profileService.GetUserProfileAsync(userId);
                return Ok(profile);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Profiel voor gebruiker {userId} niet gevonden.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserProfileResponseDto>> Create([FromBody] UserProfileRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _profileService.CreateUserProfileAsync(userId, dto);

            return CreatedAtAction(nameof(GetByUserId), new { userId = result.UserId }, result);
        }

        [HttpPut]
        public async Task<ActionResult<UserProfileResponseDto>> Update([FromBody] UserProfileRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            try
            {
                var updated = await _profileService.UpdateUserProfileAsync(userId, dto);
                return Ok(updated);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Profiel voor gebruiker {userId} niet gevonden.");
            }
        }

      
    }
}

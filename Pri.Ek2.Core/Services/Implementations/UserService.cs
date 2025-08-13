using Pri.Ek2.Core.Dtos.RequestDtos;
using Pri.Ek2.Core.Dtos.ResponseDtos;
using Pri.Ek2.Core.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pri.Ek2.Core.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<UserService> _logger;

        public UserService(UserManager<IdentityUser> userManager, ILogger<UserService> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var userDtos = new List<UserDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userDtos.Add(new UserDto
                {
                    UserId = user.Id,
                    Email = user.Email,
                    FirstName = "",
                    LastName = "",
                    Role = roles.FirstOrDefault() ?? ""
                });
            }

            return userDtos;
        }

        public async Task<UserDto> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning("Gebruiker met ID {UserId} niet gevonden.", userId);
                return null;
            }

            var roles = await _userManager.GetRolesAsync(user);
            return new UserDto
            {
                UserId = user.Id,
                Email = user.Email,
                FirstName = "",
                LastName = "",
                Role = roles.FirstOrDefault() ?? ""
            };
        }

        public async Task ResetPasswordAsync(ResetPasswordRequestDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);
            if (user == null)
            {
                _logger.LogWarning("Wachtwoord reset mislukt: gebruiker met ID {UserId} niet gevonden.", dto.UserId);
                throw new KeyNotFoundException("Gebruiker niet gevonden.");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, dto.NewPassword);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                _logger.LogError("Wachtwoord reset mislukt voor gebruiker {UserId}: {Errors}", dto.UserId, errors);
                throw new System.Exception($"Wachtwoord reset mislukt: {errors}");
            }
            _logger.LogInformation("Wachtwoord succesvol gereset voor gebruiker {UserId}.", dto.UserId);
        }
    }
}

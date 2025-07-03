using Microsoft.AspNetCore.Identity;
using Pri.Ek2.Core.Dtos.AuthDtos;
using Pri.Ek2.Core.Dtos.ResponseDtos;
using Pri.Ek2.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ek2.Core.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthService(ITokenService tokenService, UserManager<IdentityUser> userManager)
        {
            _tokenService = tokenService;
            _userManager = userManager;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
        {
            var user = new IdentityUser { UserName = loginRequestDto.Email, Email = loginRequestDto.Email };
            var result = await _userManager.CreateAsync(user, loginRequestDto.Password);

            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

            return new AuthResponseDto
            {
                Token = _tokenService.GenerateToken(user),
                Expiration = DateTime.Now.AddHours(2),
                UserProfile = new UserProfileResponseDto
                {
                    Email = user.Email,
                    Password = loginRequestDto.Password
                }
            };
        }

        public Task<AuthResponseDto> RegisterAsync(RegisterRequestDto registerRequestDto)
        {
            var user = new IdentityUser { UserName = registerRequestDto.Email, Email = registerRequestDto.Email };
            var result = _userManager.CreateAsync(user, registerRequestDto.Password).Result;
            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            return Task.FromResult(new AuthResponseDto
            {
                Token = _tokenService.GenerateToken(user),
                Expiration = DateTime.Now.AddHours(2),
                UserProfile = new UserProfileResponseDto
                {
                    Email = user.Email,
                    Password = registerRequestDto.Password,
                    FirstName = registerRequestDto.FirstName,
                    LastName = registerRequestDto.LastName,
                    BirthData = registerRequestDto.BirthDate
                }
            });
        }
    }
}

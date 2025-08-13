using Microsoft.AspNetCore.Identity;
using Pri.Ek2.Core.Data;
using Pri.Ek2.Core.Dtos.AuthDtos;
using Pri.Ek2.Core.Dtos.ResponseDtos;
using Pri.Ek2.Core.Services.Interfaces;
using Pri.Ek2.Core.Entities;


namespace Pri.Ek2.Core.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _applicationDbContext;

        public AuthService(
            ITokenService tokenService,
            UserManager<IdentityUser> userManager,
            ApplicationDbContext applicationDbContext)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
        {
            // Zoek gebruiker op email
            var user = await _userManager.FindByEmailAsync(loginRequestDto.Email);
            if (user == null)
                throw new Exception("Invalid login attempt.");

            // Check wachtwoord
            var passwordValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
            if (!passwordValid)
                throw new Exception("Invalid login attempt.");

            // Haal rollen op
            var roles = await _userManager.GetRolesAsync(user);

            return new AuthResponseDto
            {
                Token = _tokenService.GenerateToken(user, roles),
                Expiration = DateTime.Now.AddHours(2),
                UserProfile = new UserProfileResponseDto
                {
                    Email = user.Email
                }
            };
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto registerRequestDto)
        {
            var user = new IdentityUser
            {
                UserName = registerRequestDto.Email,
                Email = registerRequestDto.Email
            };

            var result = await _userManager.CreateAsync(user, registerRequestDto.Password);
            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

            // Voeg rol toe
            var role = string.IsNullOrEmpty(registerRequestDto.Role) ? "User" : registerRequestDto.Role;
            await _userManager.AddToRoleAsync(user, role);

            // Voeg UserProfile toe aan database
            var profile = new UserProfile
            {
                UserId = user.Id,
                Email = user.Email,
                FirstName = registerRequestDto.FirstName,
                LastName = registerRequestDto.LastName,
                BirthData = registerRequestDto.BirthDate,
                ProfileImagePath = "images/default.png"
            };

            _applicationDbContext.UserProfiles.Add(profile);
            await _applicationDbContext.SaveChangesAsync();

            var roles = await _userManager.GetRolesAsync(user);

            return new AuthResponseDto
            {
                Token = _tokenService.GenerateToken(user, roles),
                Expiration = DateTime.Now.AddHours(2),
                UserProfile = new UserProfileResponseDto
                {
                    Email = profile.Email,
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    BirthData = profile.BirthData,
                    ProfileImagePath = profile.ProfileImagePath
                }
            };
        }
    }
}

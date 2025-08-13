using Pri.Ek2.Core.Dtos.AuthDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ek2.Core.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterRequestDto registerRequestDto);
        Task<AuthResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
    }
}

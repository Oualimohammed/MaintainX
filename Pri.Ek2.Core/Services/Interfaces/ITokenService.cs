using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Pri.Ek2.Core.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(IdentityUser identityUser, IList<string> roles);
    }
}

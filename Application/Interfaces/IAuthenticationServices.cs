using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthenticationServices
    {
        Task<bool> ValidateUserAsync(UserForAuthenticationDto dto);
        Task<string> CreateTokenAsync();
        Task<IdentityResult> RegisterUserAsync(RegisterUserDto dto);
        Task<User> GetUserByUsernameAsync(string username);
    }
}

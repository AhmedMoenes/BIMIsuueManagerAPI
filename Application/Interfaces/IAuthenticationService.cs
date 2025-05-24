using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> ValidateUserAsync(UserForAuthenticationDto dto);
        Task<string> CreateTokenAsync();
        Task<IdentityResult> RegisterUserAsync(RegisterUserDto dto);
        Task<User> GetUserByUsernameAsync(string username);
    }
}

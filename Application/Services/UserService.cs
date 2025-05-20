using Application.DTOs.Users;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly UserManager<User> _userManager;

        public UserService(IUserRepository userRepo, UserManager<User> userManager)
        {
            _userRepo = userRepo;
            _userManager = userManager;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            IEnumerable<User> users = await _userRepo.GetAllAsync();
            return users.Select(u => new UserDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                UserName = u.UserName,
                CompanyId = u.CompanyId
            });
        }

        public async Task<UserDto> GetByIdAsync(string id)
        {
            User u = await _userRepo.GetByIdAsync(id);
            return new UserDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                UserName = u.UserName,
                CompanyId = u.CompanyId
            };
        }

        public async Task<UserDto> RegisterAsync(RegisterUserDto dto)
        {
            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                UserName = dto.UserName,
                Email = dto.Email,
                CompanyId = dto.CompanyId
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Registration failed: {errors}");
            }

            if (!string.IsNullOrEmpty(dto.Role))
            {
                await _userManager.AddToRoleAsync(user, dto.Role);
            }

            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                CompanyId = user.CompanyId
            };
        }

        public async Task<bool> UpdateAsync(string id, UpdateUserDto dto)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null) return false;

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;

            return await _userRepo.UpdateAsync(user);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await _userRepo.DeleteAsync(id);
        }

        public async Task<IEnumerable<UserOverviewDto>> GetUserOverviewAsync()
        {
            return await _userRepo.GetUserOverviewAsync(async user =>
            {
                var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

                return new UserOverviewDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    CompanyName = user.Company?.CompanyName,
                    Role = role,
                    CreatedOn = user.CreatedOn,
                    ProjectsIncludedCount = user.ProjectMemberships?.Count ?? 0,
                    IssuesCreatedCount = user.CreatedIssues?.Count ?? 0
                };
            });
        }

        public async Task<int> GetCompanyIdAsync(string userId)
        {
            User user = await _userRepo.GetByIdAsync(userId);
            return user.CompanyId;

        }
    }
}

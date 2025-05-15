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

        public async Task RegisterAsync(RegisterUserDto dto)
        {
            User user = new User
            {
                UserName = dto.UserName,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                CompanyId = dto.CompanyId
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"User creation failed: {errors}");
            }

            if (!string.IsNullOrEmpty(dto.Role))
            {
                await _userManager.AddToRoleAsync(user, dto.Role);
            }
        }

        public async Task UpdateAsync(string id, UpdateUserDto dto)
        {
            User user = await _userRepo.GetByIdAsync(id);

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.CompanyId = dto.CompanyId;

            _userRepo.Update(user);
            await _userRepo.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            User user = await _userRepo.GetByIdAsync(id);
            _userRepo.Delete(user);
            await _userRepo.SaveChangesAsync();
        }
    }
}

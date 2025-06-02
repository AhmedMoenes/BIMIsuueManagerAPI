using Domain.Entities;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtService _jwtService;

        public UserService(IUserRepository userRepo,
                           UserManager<User> userManager,
                           IJwtService jwtService,
                           SignInManager<User> signInManager)
        {
            _userRepo = userRepo;
            _userManager = userManager;
            _jwtService = jwtService;
            _signInManager = signInManager;
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

        public async Task<UserOverviewDto> GetByIdAsync(string id)
        {
            User user = await _userRepo.GetUserOverviewByIdAsync(id);
            string role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

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

        }

        public async Task<IEnumerable<CompanyUserDto>> GetCompanyUsers(int companyId)
        {
            IEnumerable<User> companyUsers = await _userRepo.GetUsersByCompanyAsync(companyId);
            List<CompanyUserDto> userDtos = new List<CompanyUserDto>();
            foreach (var user in companyUsers)
            {
                var roles = await _userManager.GetRolesAsync(user);

                userDtos.Add(new CompanyUserDto
                {
                    FullName = $"{user.FirstName} {user.LastName}",
                    Email = user.Email,
                    Projects = string.Join(", ", user.ProjectMemberships.Select(pm => pm.Project.ProjectName)),
                    Role = roles.FirstOrDefault()
                });
            }
            return userDtos;
        }

        // To Be Deleted
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
                CompanyId = user.CompanyId,
                Role = dto.Role
            };
        }

        public async Task<UserDto> CreateUserWithProjectsAsync(string adminUserId, CreateUserWithProjectsDto dto)
        {
            int companyId = await _userRepo.GetCompanyIdAsync(adminUserId);

            RegisterUserDto registerUser = new RegisterUserDto()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                UserName = dto.UserName,
                Password = dto.Password,
                Role = dto.Role,
                CompanyId = companyId
            };

            UserDto user = await RegisterAsync(registerUser);

            var memberships = dto.ProjectAssignments.Select(p => new ProjectTeamMember
            {
                ProjectId = p.ProjectId,
                Role = p.RoleInProject
            }).ToList();

            await _userRepo.AddUserToProjectsAsync(user.Id, memberships);

            return user;
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

        public async Task<IEnumerable<UserOverviewDto>> GetUsersOverviewAsync()
        {
            return await _userRepo.GetUsersOverviewAsync(async user =>
            {
                string role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

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

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                throw new UnauthorizedAccessException("Invalid credentials");

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded)
                throw new UnauthorizedAccessException("Invalid credentials");

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtService.GenerateToken(user, roles.First());

            return new LoginResponseDto
            {
                Token = token,
                Role = roles.First(),
                FullName = $"{user.FirstName} {user.LastName}",
                Email = user.Email
            };
        }

        public async Task<UserDto?> GetByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return null;

            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,

                UserName = user.UserName,
                CompanyId = user.CompanyId
            };
        }

        public async Task<IEnumerable<UserDto>> GetByProjectIdAsync(int projectId)
        {
            var users = await _userRepo.GetByProjectIdAsync(projectId);
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

    }
}

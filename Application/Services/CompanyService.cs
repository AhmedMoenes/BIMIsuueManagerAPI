using Application.DTOs.Companies;
using Application.DTOs.Users;
using Domain.Constants;

namespace Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _repo;
        private readonly IUserService _userService;

        public CompanyService(ICompanyRepository repo, IUserService userService)
        {
            _repo = repo;
            _userService = userService;
        }

        public async Task<IEnumerable<CompanyDto>> GetAllAsync()
        {
            IEnumerable<Company> companies = await _repo.GetAllAsync();
            return companies.Select(c => new CompanyDto
            {
                CompanyId = c.CompanyId,
                CompanyName = c.CompanyName,
            });
        }

        public async Task<CompanyDto> GetByIdAsync(int id)
        {
            Company c = await _repo.GetByIdAsync(id);
            return new CompanyDto
            {
                CompanyId = c.CompanyId,
                CompanyName = c.CompanyName,
            };
        }

        public async Task<CompanyDto> CreateAsync(CreateCompanyDto dto)
        {
            var company = new Company
            {
                CompanyName = dto.CompanyName,
            };

            var created = await _repo.AddAsync(company);

            return new CompanyDto
            {
                CompanyId = created.CompanyId,
                CompanyName = created.CompanyName,
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateCompanyDto dto)
        {
            var company = await _repo.GetByIdAsync(id);
            if (company == null) return false;

            company.CompanyName = dto.CompanyName;

            return await _repo.UpdateAsync(company);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }

        public async Task<IEnumerable<CompanyOverviewDto>> GetCompaniesForUserAsync(string userId)
        {
            return await _repo.GetUserCompaniesAsync(userId, async company =>
            {
                return new CompanyOverviewDto
                {
                    CompanyId = company.CompanyId,
                    CompanyName = company.CompanyName,
                    UsersCount = company.Users?.Count ?? 0,
                    ProjectsCount = company.Projects?.Count ?? 0,
                    IssuesCount = company.Projects?.Sum(p => p.Issues.Count) ?? 0
                };
            });
        }

        public async Task<CompanyDto> CreateCompanyWithAdminAsync(CreateCompanyWithAdminDto dto)
        {
            Company company = new Company
            {
                CompanyName = dto.CompanyName

            };

            Company createdCompany = await _repo.AddAsync(company);

            RegisterUserDto user = new RegisterUserDto
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                UserName = dto.UserName,
                Role = UserRoles.Admin,
                CompanyId = createdCompany.CompanyId,
            };

            UserDto companyAdmin = await _userService.RegisterAsync(user);

            return new CompanyDto
            {
                CompanyId = createdCompany.CompanyId,
                CompanyName = createdCompany.CompanyName
            };
        }
    }
}

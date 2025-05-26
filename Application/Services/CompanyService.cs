namespace Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _repo;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        public CompanyService(ICompanyRepository repo,
                              IUserService userService,
                              IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _userService = userService;
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<CompanyDto>> GetAllAsync()
        {
            IEnumerable<Company> companies = await _repo.GetAllWithProjectsAsync();
            return companies.Select(c => new CompanyDto
            {
                CompanyId = c.CompanyId,
                CompanyName = c.CompanyName,
                Projects = c.CompanyProjects
                           .Select(cp => new ProjectSummaryDto
                           {
                               ProjectId = cp.Project.ProjectId,
                               ProjectName = cp.Project.ProjectName
                           }).ToList()
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
            await _unitOfWork.SaveChangesAsync();

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
                    ProjectsCount = company.CompanyProjects.Select(cp => cp.ProjectId).Count(),
                    IssuesCount = company.CompanyProjects
                                  .SelectMany(cp => cp.Project.Issues)
                                  .Count()
                };
            });
        }
        public async Task<CompanyDto> CreateCompanyWithAdminAsync(CreateCompanyWithAdminDto dto)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                Company company = new Company
                {
                    CompanyName = dto.CompanyName
                };

                Company createdCompany = await _repo.AddAsync(company);
                await _repo.SaveChangesAsync();

                RegisterUserDto user = new RegisterUserDto
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.Email,
                    Password = dto.Password,
                    UserName = dto.UserName,
                    CompanyId = createdCompany.CompanyId,
                    Role = UserRoles.CompanyAdmin,
                };

                UserDto companyAdmin = await _userService.RegisterAsync(user);

                CompanyDto companyDto = new CompanyDto
                {
                    CompanyId = createdCompany.CompanyId,
                    CompanyName = createdCompany.CompanyName
                };

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                return companyDto;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}

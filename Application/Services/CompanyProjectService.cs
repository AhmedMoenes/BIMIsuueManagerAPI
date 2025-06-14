namespace Application.Services
{
    public class CompanyProjectService : ICompanyProjectService
    {
        private readonly ICompanyProjectRepository _repo;
        private readonly IUnitOfWork _unitOfWork;

        public CompanyProjectService(ICompanyProjectRepository repo, IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
        }

        public async Task AssignCompaniesAsync(AssignCompaniesToProjectDto dto)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                await _repo.AssignCompaniesToProjectAsync(dto.ProjectId, dto.CompanyIds);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<ProjectOverviewDto>> GetForCompanyAsync(int companyId)
        {
            IEnumerable<Project> allProjects = await _repo.GetByCompanyIdAsync(companyId);

            IEnumerable<ProjectOverviewDto> projects = allProjects.Select(project => new ProjectOverviewDto
            {
                ProjectId = project.ProjectId,
                ProjectName = project.ProjectName,
                Description = project.Description,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                IssuesCount = project.Issues?.Count ?? 0,
                MembersCount = project.ProjectTeamMembers
                               .Select(pm => pm.User.FirstName)
                               .Count(),
                CompanyNames = project.CompanyProjects
                               .Select(c => c.Company.CompanyName)
                               .Distinct()
                               .ToList()
            });
            return projects;
        }
    }
}

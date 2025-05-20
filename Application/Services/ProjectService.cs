namespace Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _repo;

        public ProjectService(IProjectRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ProjectDto>> GetAllAsync()
        {
            IEnumerable<Project> projects = await _repo.GetAllAsync();
            return projects.Select(p => new ProjectDto
            {
                ProjectId = p.ProjectId,
                ProjectName = p.ProjectName,
                CompanyId = p.CompanyId,
                StartDate = p.StartDate,
                EndDate = p.EndDate
            });
        }

        public async Task<ProjectDto> GetByIdAsync(int id)
        {
            Project p = await _repo.GetByIdAsync(id);
            return new ProjectDto
            {
                ProjectId = p.ProjectId,
                ProjectName = p.ProjectName,
                CompanyId = p.CompanyId,
                StartDate = p.StartDate,
                EndDate = p.EndDate
            };
        }

        public async Task<ProjectDto> CreateAsync(CreateProjectDto dto)
        {
            var project = new Project
            {
                ProjectName = dto.ProjectName,
                CompanyId = dto.CompanyId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate
            };

            var created = await _repo.AddAsync(project);

            return new ProjectDto
            {
                ProjectId = created.ProjectId,
                ProjectName = created.ProjectName,
                CompanyId = created.CompanyId,
                StartDate = created.StartDate,
                EndDate = created.EndDate
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateProjectDto dto)
        {
            var p = await _repo.GetByIdAsync(id);
            if (p == null) return false;

            p.ProjectName = dto.ProjectName;
            p.CompanyId = dto.CompanyId;
            p.StartDate = dto.StartDate;
            p.EndDate = dto.EndDate;

            return await _repo.UpdateAsync(p);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }

        public async Task<IEnumerable<ProjectOverviewDto>> GetForSubscriberAsync()
        {
            return await _repo.GetProjectOverviewsAsync(async project =>
            {
                return new ProjectOverviewDto
                {
                    ProjectId = project.ProjectId,
                    ProjectName = project.ProjectName,
                    Description = project.Description,
                    StartDate = project.StartDate,
                    EndDate = project.EndDate,
                    IssuesCount = project.Issues?.Count ?? 0,
                    CompanyNames = project.ProjectTeamMembers
                        .Select(m => m.User.Company.CompanyName)
                        .Distinct()
                        .ToList()
                };
            });
        }

        public async Task<IEnumerable<ProjectOverviewDto>> GetForCompanyAsync(int companyId)
        {
            var all = await _repo.GetProjectOverviewsAsync(async project =>
            {
                return new ProjectOverviewDto
                {
                    ProjectId = project.ProjectId,
                    ProjectName = project.ProjectName,
                    Description = project.Description,
                    StartDate = project.StartDate,
                    EndDate = project.EndDate,
                    IssuesCount = project.Issues?.Count ?? 0,
                    CompanyNames = project.ProjectTeamMembers
                        .Select(m => m.User.Company.CompanyName)
                        .Distinct()
                        .ToList()
                };
            });

            return all.Where(project =>
                project.CompanyNames != null &&
                project.CompanyNames.Count > 0 &&
                project.CompanyNames.Any()
            ).ToList();
        }

        public async Task<IEnumerable<ProjectOverviewDto>> GetForUserAsync(string userId)
        {
            var all = await _repo.GetProjectOverviewsAsync(async project =>
            {
                return new ProjectOverviewDto()
                {
                    ProjectId = project.ProjectId,
                    ProjectName = project.ProjectName,
                    Description = project.Description,
                    StartDate = project.StartDate,
                    EndDate = project.EndDate,
                    IssuesCount = project.Issues?.Count ?? 0,
                    CompanyNames = project.ProjectTeamMembers
                        .Select(m => m.User.Company.CompanyName)
                        .Distinct()
                        .ToList(),
                    UserRoleInProject = project.ProjectTeamMembers
                        .FirstOrDefault(m => m.UserId == userId)?.Role
                };
            });
        return all.Where(p => p.UserRoleInProject != null);
    }

}
}

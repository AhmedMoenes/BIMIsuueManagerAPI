using Application.DTOs.Projects;

namespace Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepo;

        public ProjectService(IProjectRepository projectRepo)
        {
            _projectRepo = projectRepo;
        }

        public async Task<IEnumerable<ProjectDto>> GetAllAsync()
        {
            IEnumerable<Project> projects = await _projectRepo.GetAllAsync();
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
            Project p = await _projectRepo.GetByIdAsync(id);
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
            Project project = new Project
            {
                ProjectName = dto.ProjectName,
                Description = dto.Description,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                CompanyId = dto.CompanyId
            };

            IEnumerable<ProjectTeamMember> teamMembers = dto.TeamMemberUserIds.Select(userId => new ProjectTeamMember
            {
                ProjectId = project.ProjectId,
                UserId = userId
            }).ToList();

            IEnumerable<Label> labels = dto.Labels.Select(label => new Label
            {
                LabelName = label.LabelName,
                ProjectId = project.ProjectId
            }).ToList();
            IEnumerable<Area> areas = dto.Areas.Select(area => new Area
            {
                AreaName = area.AreaName,
                ProjectId = project.ProjectId
            }).ToList();

            Project created = await _projectRepo.AddAsync(project);

            return new ProjectDto
            {
                ProjectId = created.ProjectId,
                ProjectName = created.ProjectName,
                Description = created.Description,
                StartDate = created.StartDate,
                EndDate = created.EndDate,
                CompanyId = created.CompanyId
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateProjectDto dto)
        {
            var p = await _projectRepo.GetByIdAsync(id);
            if (p == null) return false;

            p.ProjectName = dto.ProjectName;
            p.CompanyId = dto.CompanyId;
            p.StartDate = dto.StartDate;
            p.EndDate = dto.EndDate;

            return await _projectRepo.UpdateAsync(p);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _projectRepo.DeleteAsync(id);
        }

        public async Task<IEnumerable<ProjectOverviewDto>> GetForSubscriberAsync()
        {
            return await _projectRepo.GetProjectOverviewsAsync(async project =>
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
            var all = await _projectRepo.GetProjectOverviewsAsync(async project =>
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
            var all = await _projectRepo.GetProjectOverviewsAsync(async project =>
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

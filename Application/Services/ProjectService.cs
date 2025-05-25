namespace Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepo;
        private readonly ILabelRepository _labelRepo;
        private readonly IAreaRepository _areaRepo;
        private readonly IProjectTeamMemberRepository _teamRepo;
        private readonly IUnitOfWork _unitOfWork;

        public ProjectService(IProjectRepository projectRepo,
                              ILabelRepository labelRepo,
                              IAreaRepository areaRepo,
                              IProjectTeamMemberRepository teamRepo,
                              IUnitOfWork unitOfWork)
        {
            _projectRepo = projectRepo;
            _labelRepo = labelRepo;
            _areaRepo = areaRepo;
            _teamRepo = teamRepo;
            _unitOfWork = unitOfWork;
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
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                Project project = new Project
                {
                    ProjectName = dto.ProjectName,
                    Description = dto.Description,
                    StartDate = dto.StartDate,
                    EndDate = dto.EndDate,
                    CompanyId = dto.CompanyId
                };

                Project createdProject = await _projectRepo.AddAsync(project);
                await _unitOfWork.SaveChangesAsync();

                IEnumerable<Label> labels = dto.Labels?.Select(LabelDto => new Label
                {
                    LabelName = LabelDto.LabelName,
                    ProjectId = createdProject.ProjectId
                });

                if (labels != null && labels.Any())
                    await _labelRepo.AddRangeAsync(labels);

                IEnumerable<Area> areas = dto.Areas?.Select(AreaDto => new Area
                {
                    AreaName = AreaDto.AreaName,
                    ProjectId = createdProject.ProjectId
                }).ToList();

                if (areas != null && areas.Any())
                    await _areaRepo.AddRangeAsync(areas);


                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                return new ProjectDto()
                {
                    ProjectId = createdProject.ProjectId,
                    ProjectName = createdProject.ProjectName,
                    Description = createdProject.Description,
                    StartDate = createdProject.StartDate,
                    EndDate = createdProject.EndDate,
                    CompanyId = createdProject.CompanyId
                };
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
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

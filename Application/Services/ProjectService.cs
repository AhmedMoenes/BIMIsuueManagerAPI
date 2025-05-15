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

        public async Task CreateAsync(CreateProjectDto dto)
        {
            Project project = new Project
            {
                ProjectName = dto.ProjectName,
                CompanyId = dto.CompanyId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate
            };

            await _repo.AddAsync(project);
            await _repo.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, UpdateProjectDto dto)
        {
            Project project = await _repo.GetByIdAsync(id);
            project.ProjectName = dto.ProjectName;
            project.CompanyId = dto.CompanyId;
            project.StartDate = dto.StartDate;
            project.EndDate = dto.EndDate;

            _repo.Update(project);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var project = await _repo.GetByIdAsync(id);
            _repo.Delete(project);
            await _repo.SaveChangesAsync();
        }
    }
}

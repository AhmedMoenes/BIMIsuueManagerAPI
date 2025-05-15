namespace Application.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDto>> GetAllAsync();
        Task<ProjectDto> GetByIdAsync(int id);
        Task CreateAsync(CreateProjectDto dto);
        Task UpdateAsync(int id, UpdateProjectDto dto);
        Task DeleteAsync(int id);
    }
}

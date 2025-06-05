namespace Application.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDto>> GetAllAsync();
        Task<ProjectDto> CreateAsync(CreateProjectDto dto);   
        Task<bool> UpdateAsync(int id, UpdateProjectDto dto); 
        Task<bool> DeleteAsync(int id);                       
        Task<ProjectDto> GetByIdAsync(int id);
        Task<IEnumerable<ProjectOverviewDto>> GetForSubscriberAsync();
        Task<IEnumerable<ProjectOverviewDto>>GetForCompanyAsync(int companyId);
        Task<IEnumerable<ProjectOverviewDto>> GetForUserAsync(string userId);
        Task<IEnumerable<ProjectDto>> GetByUserIdAsync(string userId);
    }
}

namespace Application.Interfaces
{
    public interface IIssueService
    {
        Task<IEnumerable<IssueDto>> GetAllAsync();
        Task<IssueDto> GetByIdAsync(int id);
        Task<IssueDto> CreateAsync(CreateIssueDto dto);     
        Task<bool> UpdateAsync(int id, UpdateIssueDto dto); 
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<IssueDto>> GetByProjectIdAsync(int projectId);
        Task<IEnumerable<IssueDto>> GetByUserIdAsync(string userId);

    }
}

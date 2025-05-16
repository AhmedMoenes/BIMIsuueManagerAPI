namespace Application.Interfaces
{
    public interface IIssueService
    {
        Task<IEnumerable<IssueDto>> GetAllAsync();
        Task<IssueDto> GetByIdAsync(int id);
        Task CreateAsync(CreateIssueDto dto);
        Task UpdateAsync(int id, UpdateIssueDto dto);
        Task DeleteAsync(int id);
    }
}

namespace Application.Interfaces
{
    public interface IIssueService
    {
        Task<IEnumerable<IssueDto>> GetAllAsync();
        Task<IssueDto> GeyByIdAsync(int id);
        Task CreateAsync(CreateIssueDto dto);
        Task UpdateAsync(int id, UpdateIssueDto dto);
        Task DeleteAsync(int id);
    }
}

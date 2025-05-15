namespace Application.Interfaces
{
    public interface IRevitElementService
    {
        Task<IEnumerable<RevitElementDto>> GetByIssueIdAsync(int issueId);
        Task AddAsync(RevitElementDto dto);
        Task DeleteAsync(int id);
    }
}

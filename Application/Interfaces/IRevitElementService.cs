namespace Application.Interfaces
{
    public interface IRevitElementService
    {
        Task<IEnumerable<RevitElementDto>> GetByIssueIdAsync(int issueId);
        Task<RevitElementDto> AddAsync(RevitElementDto dto);   
        Task<bool> DeleteAsync(int id);                        
    }
}


namespace Application.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDto>> GetAllAsync();
        Task<CommentDto> GetByIdAsync(int id);
        Task <CommentDto> CreateAsync(CreateCommentDto dto);
        Task<IEnumerable<CommentDto>> GetByIssueIdAsync(int issueId);
        Task<IEnumerable<CommentDto>> GetBySnapshotIdAsync(int snapshotId);
        Task<bool> UpdateAsync(int id, CommentDto dto);
        Task<bool> DeleteAsync(int id);
    }
}

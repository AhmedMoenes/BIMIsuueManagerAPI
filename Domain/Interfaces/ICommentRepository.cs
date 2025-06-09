using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<IEnumerable<Comment>> GetByIssueIdAsync(int issueId);
        Task<IEnumerable<Comment>> GetBySnapshotIdAsync(int snapshotId);
    }
}

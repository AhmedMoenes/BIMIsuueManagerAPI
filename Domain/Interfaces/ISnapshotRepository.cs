using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ISnapshotRepository : IRepository<Snapshot>
    {
        Task<List<Snapshot>> GetByIssueIdAsync(int issueId);
    }
}

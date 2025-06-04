using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISnapshotRepository : IRepository<Snapshot>
    {
        Task<List<Snapshot>> GetByIssueIdAsync(int issueId);
    }
}

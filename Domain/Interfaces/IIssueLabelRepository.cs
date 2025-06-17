using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Interfaces
{
    public interface IIssueLabelRepository : IRepository<IssueLabel>
    {
        public Task DeleteByIssueIdAsync(int issueId);
    }
}

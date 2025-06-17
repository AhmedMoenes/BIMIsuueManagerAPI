using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class IssueLabelRepository : Repository<IssueLabel>, IIssueLabelRepository
    {
        public IssueLabelRepository(AppDbContext context) : base(context) { }

        public async Task DeleteByIssueIdAsync(int issueId)
        {
            var labels = await Context.IssueLabels.Where(l => l.IssueId == issueId).ToListAsync();
            Context.IssueLabels.RemoveRange(labels);
        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(AppDbContext context) : base(context) { }
        public async Task<IEnumerable<Comment>> GetByIssueIdAsync(int issueId)
        {
            return await Context.Comments
                .Where(c => c.IssueId == issueId)
                .Include(c => c.Issue)          
                .Include(c => c.Snapshot)       
                .Include(c => c.CreatedByUser)  
                .ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetBySnapshotIdAsync(int snapshotId)
        {
            return await Context.Comments
                .Where(c => c.SnapshotId == snapshotId)
                .Include(c => c.Issue)
                .Include(c => c.Snapshot)
                .Include(c => c.CreatedByUser)
                .ToListAsync();
        }

        public async Task<Comment> GetByIdWithUserAsync(int commentId)
        {
            return await Context.Comments
                .Include(c => c.CreatedByUser)
                .Include(c => c.Issue)
                .Include(c => c.Snapshot)
                .FirstOrDefaultAsync(c => c.CommentId == commentId);
        }
    }
}
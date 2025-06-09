namespace Infrastructure.Repositories
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(AppDbContext context) : base(context) { }
        public async Task<IEnumerable<Comment>> GetByIssueIdAsync(int issueId)
        {
            return await Context.Comments
                .Where(c => c.IssueId == issueId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetBySnapshotIdAsync(int snapshotId)
        {
            return await Context.Comments
                .Where(c => c.SnapshotId == snapshotId)
                .ToListAsync();
        }
    }
}
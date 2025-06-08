namespace Infrastructure.Repositories
{
    public class SnapshotRepository : Repository<Snapshot>, ISnapshotRepository
    {
        private readonly AppDbContext _context;

        public SnapshotRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Snapshot>> GetByIssueIdAsync(int issueId)
        {
            return await _context.Snapshots
                .Where(s => s.IssueId == issueId)
                .ToListAsync();
        }

    }
}

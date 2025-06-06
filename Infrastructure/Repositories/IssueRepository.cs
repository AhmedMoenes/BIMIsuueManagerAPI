using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class IssueRepository : Repository<Issue>, IIssueRepository
    {
        public readonly IRevitElementRepository _revitElementRepository;
        private readonly IIssueLabelRepository _issueLabelRepository;
        private readonly ISnapshotRepository _snapshotRepository;

        public IssueRepository(AppDbContext context, ICommentRepository commentRepository,
                               IRevitElementRepository revitElementRepository,
                               IIssueLabelRepository issueLabelRepository,
                               ISnapshotRepository snapshotRepository) : base(context)
        {
            _revitElementRepository = revitElementRepository;
            _issueLabelRepository = issueLabelRepository;
            _snapshotRepository = snapshotRepository;
        }

        public async Task<Issue> CreateDetailedAsync<T>(Issue issue,
            List<Snapshot> snapshots,
            List<RevitElement> revitElements,
            List<IssueLabel> issueLabels)
        {
            await Context.Issues.AddAsync(issue);

            if (revitElements?.Any() == true)
            {
                foreach (RevitElement r in revitElements)
                {
                    r.IssueId = issue.IssueId;
                    await _revitElementRepository.AddAsync(r);
                }
            }

            if (snapshots?.Any() == true)
            {
                foreach (Snapshot s in snapshots)
                {
                    s.IssueId = issue.IssueId;
                    await _snapshotRepository.AddAsync(s);
                }
            }

            if (issueLabels?.Any() == true)
            {
                foreach (IssueLabel l in issueLabels)
                {
                    l.IssueId = issue.IssueId;
                    await _issueLabelRepository.AddAsync(l);
                }
            }
            return issue;
        }

        public async Task<IEnumerable<Issue>> GetAllDetailed()
        {
            return await Context.Issues
                .Include(i => i.Area)
                .Include(i => i.Project)
                .Include(i => i.Labels)
                .ThenInclude(il => il.Label)
                .Include(i => i.Comments)
                .ThenInclude(c => c.CreatedByUser)
                .Include(i => i.RevitElements)
                .Include(i => i.CreatedByUser)
                .Include(i => i.AssignedToUser)
                .ToListAsync();
        }

        public async Task<Issue> GetByIdDetailed(int id)
        {
            return await Context.Issues
                .Include(i => i.Area)
                .Include(i => i.Project)
                .Include(i => i.Labels)
                .ThenInclude(il => il.Label)
                .Include(i => i.Comments)
                .ThenInclude(c => c.CreatedByUser)
                .Include(i => i.RevitElements)
                .Include(i => i.CreatedByUser)
                .Include(i => i.AssignedToUser)
                .FirstOrDefaultAsync(i => i.IssueId == id);
        }

        public async Task<IEnumerable<Issue>> GetByProjectIdDetailedAsync(int projectId)
        {
            return await Context.Issues
                .Where(i => i.ProjectId == projectId)
                .Include(i => i.Snapshots)
                .Include(i => i.Comments).ThenInclude(c => c.CreatedByUser)
                .Include(i => i.Area)
                .Include(i => i.Labels).ThenInclude(l => l.Label)
                .Include(i => i.RevitElements)
                .Include(i => i.CreatedByUser)
                .Include(i => i.AssignedToUser)
                .Include(i => i.Project)
                .ToListAsync();
        }

        public async Task<IEnumerable<Issue>> GetByUserIdDetailedAsync(string userId)
        {
            return await Context.Issues
                .Where(i => i.AssignedToUserId == userId || i.CreatedByUserId == userId)
                .Include(i => i.Snapshots)
                .Include(i => i.Comments).ThenInclude(c => c.CreatedByUser)
                .Include(i => i.Area)
                .Include(i => i.Labels).ThenInclude(l => l.Label)
                .Include(i => i.RevitElements)
                .Include(i => i.CreatedByUser)
                .Include(i => i.AssignedToUser)
                .Include(i => i.Project)
                .ToListAsync();
        }
    }
}

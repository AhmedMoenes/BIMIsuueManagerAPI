namespace Infrastructure.Repositories
{
    public class IssueRepository : Repository<Issue>, IIssueRepository
    {
        private readonly ICommentRepository _commentRepository;
        public readonly IRevitElementRepository _revitElementRepository;
        private readonly IIssueLabelRepository _issueLabelRepository;

        public IssueRepository(AppDbContext context, ICommentRepository commentRepository,
                               IRevitElementRepository revitElementRepository,
                               IIssueLabelRepository issueLabelRepository) : base(context)
        {
            _commentRepository = commentRepository;
            _revitElementRepository = revitElementRepository;
            _issueLabelRepository = issueLabelRepository;
        }

        public async Task<Issue> CreateDetailedAsync<T>(Issue issue,
            List<Comment> comments,
            List<RevitElement> revitElements,
            List<IssueLabel> issueLabels)
        {

            await DbSet.AddAsync(issue);
            await Context.SaveChangesAsync();

            if (comments?.Any() == true)
            {
                foreach (var c in comments)
                {
                    c.IssueId = issue.IssueId;
                    await _commentRepository.AddAsync(c);
                }
            }

            if (revitElements?.Any() == true)
            {
                foreach (var r in revitElements)
                {
                    r.IssueId = issue.IssueId;
                    await _revitElementRepository.AddAsync(r);
                }
            }

            if (issueLabels?.Any() == true)
            {
                foreach (var l in issueLabels)
                {
                    l.IssueId = issue.IssueId;
                    await _issueLabelRepository.AddAsync(l);
                }

            }

            await Context.SaveChangesAsync();
            return issue;
        }

        public async Task<IEnumerable<Issue>> GetAllDetailed()
        {
            return await Context.Issues
                .Include(i => i.Area)
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
                .Include(i => i.Labels)
                .ThenInclude(il => il.Label)
                .Include(i => i.Comments)
                .ThenInclude(c => c.CreatedByUser)
                .Include(i => i.RevitElements)
                .Include(i => i.CreatedByUser)
                .Include(i => i.AssignedToUser)
                .FirstOrDefaultAsync(i => i.IssueId == id);
        }

    }
}

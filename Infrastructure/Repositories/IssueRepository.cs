namespace Infrastructure.Repositories
{
    public class IssueRepository : Repository<Issue>, IIssueRepository
    {
        public IssueRepository(DbContext context) : base(context) { }
    }
}

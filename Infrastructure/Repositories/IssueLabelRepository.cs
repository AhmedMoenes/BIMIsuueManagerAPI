namespace Infrastructure.Repositories
{
    public class IssueLabelRepository : Repository<IssueLabel>, IIssueLabelRepository
    {
        public IssueLabelRepository(AppDbContext context) : base(context) { }
    }
}

using Domain.Repositories;

namespace Infrastructure.Repositories
{
    public class IssueLabelRepository : Repository<IssueLabel>, IIssueLabelRepository
    {
        public IssueLabelRepository(DbContext context) : base(context) { }
    }
}

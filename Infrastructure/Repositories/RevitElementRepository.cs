

namespace Infrastructure.Repositories
{
    public class RevitElementRepository : Repository<RevitElement>, IRevitElementRepository
    {
        public RevitElementRepository(DbContext context) : base(context) { }
    }
}

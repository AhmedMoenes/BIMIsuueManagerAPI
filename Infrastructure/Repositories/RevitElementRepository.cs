

namespace Infrastructure.Repositories
{
    public class RevitElementRepository : Repository<RevitElement>, IRevitElementRepository
    {
        public RevitElementRepository(AppDbContext context) : base(context) { }
    }
}


namespace Infrastructure.Repositories
{
    public class SnapshotRepository : Repository<Snapshot>, ISnapshotRepository
    {
        public SnapshotRepository(AppDbContext context) : base(context)
        {

        }
    }
}

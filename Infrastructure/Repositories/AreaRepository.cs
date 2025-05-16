namespace Infrastructure.Repositories
{
    public class AreaRepository : Repository<Area>, IAreaRepository
    {
        public AreaRepository(DbContext context) : base(context) { }
    }
}
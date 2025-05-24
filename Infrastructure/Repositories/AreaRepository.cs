namespace Infrastructure.Repositories
{
    public class AreaRepository : Repository<Area>, IAreaRepository
    {
        public AreaRepository(AppDbContext context) : base(context) { }
    }
}
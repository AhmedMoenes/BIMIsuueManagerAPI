namespace Infrastructure.Repositories
{
    public class AreaRepository : Repository<Area>, IAreaRepository
    {
        public AreaRepository(AppDbContext context) : base(context) { }
        public async Task<IEnumerable<Area>> GetByProjectIdAsync(int projectId)
        {
            return await Context.Areas
                .Where(a => a.ProjectId == projectId)
                .ToListAsync();
        }
    }
}
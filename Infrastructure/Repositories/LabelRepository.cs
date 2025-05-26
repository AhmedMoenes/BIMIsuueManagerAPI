namespace Infrastructure.Repositories
{
    public class LabelRepository : Repository<Label>, ILabelRepository
    {
        public LabelRepository(AppDbContext context) : base(context) { }
        public async Task<IEnumerable<Label>> GetByProjectIdAsync(int projectId)
        {
            return await Context.Labels
                         .Where(l => l.ProjectId == projectId)
                         .ToListAsync();
        }
    }
}

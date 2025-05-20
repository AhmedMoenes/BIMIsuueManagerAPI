namespace Infrastructure.Repositories
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(DbContext context) : base(context) { }

        public async Task<IEnumerable<T>> GetProjectOverviewsAsync<T>(Func<Project, Task<T>> selector)
        {
            var projects = await DbSet
                .Include(p => p.Issues)
                .Include(p => p.ProjectTeamMembers)
                .ThenInclude(m => m.User)
                .ThenInclude(u => u.Company)
                .ToListAsync();

            var result = new List<T>();
            foreach (var project in projects)
            {
                result.Add(await selector(project));
            }

            return result;
        }
    }
}

namespace Infrastructure.Repositories
{
    public class CompanyProjectRepository: Repository<CompanyProject>, ICompanyProjectRepository
    {
        public CompanyProjectRepository(AppDbContext context) : base(context)
        {

        }

        public async Task AssignCompaniesToProjectAsync(int projectId, List<int> companyIds)
        {
            var existing = Context.CompanyProjects.Where(cp => cp.ProjectId == projectId);
            Context.CompanyProjects.RemoveRange(existing);

            var newLinks = companyIds.Select(id => new CompanyProject
            {
                CompanyId = id,
                ProjectId = projectId
            }).ToList();

            await Context.CompanyProjects.AddRangeAsync(newLinks);
        }

        public async Task<IEnumerable<Project>> GetByCompanyIdAsync(int companyId)
        {
            return await Context.Projects
                .Where(p => p.CompanyProjects.Any(cp => cp.CompanyId == companyId))
                .Include(p => p.Issues)
                .Include(p => p.ProjectTeamMembers)
                .ThenInclude(m => m.User)
                .Include(p => p.CompanyProjects)
                .ThenInclude(cp => cp.Company)
                .ToListAsync();
        }
    }
}

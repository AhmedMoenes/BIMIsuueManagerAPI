using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        private readonly IAreaRepository _areaRepository;
        private readonly IProjectTeamMemberRepository _ProjectTeamMemberRepository;
        private readonly ILabelRepository _labelRepository;
        public ProjectRepository(AppDbContext context, IAreaRepository areaRepository,
            IProjectTeamMemberRepository iProjectTeamMemberRepository,
            ILabelRepository labelRepository) : base(context) { }

        public async Task<IEnumerable<T>> GetProjectOverviewsAsync<T>(Func<Project, Task<T>> selector)
        {
            IEnumerable<Project> projects = await DbSet
                .Include(p => p.Issues)
                .Include(p => p.ProjectTeamMembers)
                .ThenInclude(m => m.User)
                .Include(u => u.CompanyProjects)
                .ThenInclude(cp => cp.Company)
                .ToListAsync();

            List<T>result = new List<T>();
            foreach (Project project in projects)
            {
                result.Add(await selector(project));
            }

            return result;
        }
        public async Task<Project> CreateDetailedAsync<T>(Project project,
                                                          List<Area> areas,
                                                          List<Label> labels,
                                                          List<ProjectTeamMember> teamMembers)
        {
            await DbSet.AddAsync(project);
            await Context.SaveChangesAsync();

            if (teamMembers?.Any() == true)
            {
          
                foreach (ProjectTeamMember member in teamMembers)
                {
                    await _ProjectTeamMemberRepository.AddAsync(member);
                }

            }

            if (labels?.Any() == true)
            {
                foreach (Label label in labels)
                {
                    await _labelRepository.AddAsync(label);
                }

            }

            if (areas.Any() == true)
            {
                foreach (Area area in areas)
                {
                    await _areaRepository.AddAsync(area);
                }
            }

            await Context.SaveChangesAsync();
            return project;
        }
        public async Task<IEnumerable<Project>> GetAllWithCompaniesAsync()
        {
            return await Context.Projects
                         .Include(p => p.CompanyProjects)
                         .ThenInclude(cp => cp.Company).ToListAsync();
        }
        public async Task<IEnumerable<Project>> GetByUserIdAsync(string userId)
        {
            return await Context.ProjectTeamMembers
                         .Where(ptm => ptm.UserId == userId)
                         .Select(ptm => ptm.Project)
                         .Distinct()
                         .ToListAsync();
        }

        public async Task<Project?> GetByIssueIdAsync(int issueId)
        {
            return await Context.Issues
                .Where(i => i.IssueId == issueId)
                .Select(i => i.Project)
                .FirstOrDefaultAsync();
        }

    }
}

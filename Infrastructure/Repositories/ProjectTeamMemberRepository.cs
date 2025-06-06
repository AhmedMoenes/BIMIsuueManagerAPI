namespace Infrastructure.Repositories
{
    public class ProjectTeamMemberRepository : Repository<ProjectTeamMember>, IProjectTeamMemberRepository
    {
        public ProjectTeamMemberRepository(AppDbContext context) : base(context) { }
        public async Task<IEnumerable<ProjectTeamMember>> GetByProjectIdAsync(int projectId)
        {
            return await Context.ProjectTeamMembers
                         .Where(ptm => ptm.ProjectId == projectId)
                         .Include(ptm => ptm.User)
                         .Include(ptm => ptm.Project)
                         .ToListAsync();
        }

        public async Task<IEnumerable<ProjectTeamMember>> GetByUserIdAsync(string userId)
        {
            return await Context.ProjectTeamMembers
                         .Where(ptm => ptm.UserId == userId)
                         .Include(ptm => ptm.Project)
                         .Include(ptm => ptm.User)
                         .ToListAsync();
        }
    }
}

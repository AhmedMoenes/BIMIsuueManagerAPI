namespace Infrastructure.Repositories
{
    public class ProjectTeamMemberRepository : Repository<ProjectTeamMember>, IProjectTeamMemberRepository
    {
        public ProjectTeamMemberRepository(AppDbContext context) : base(context) { }
    }
}

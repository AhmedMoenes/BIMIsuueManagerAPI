namespace Infrastructure.Repositories
{
    public class ProjectTeamMemberRepository : Repository<ProjectTeamMember>, IProjectTeamMemberRepository
    {
        public ProjectTeamMemberRepository(DbContext context) : base(context) { }
    }
}

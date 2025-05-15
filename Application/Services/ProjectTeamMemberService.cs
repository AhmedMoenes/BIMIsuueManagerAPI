namespace Application.Services
{
    public class ProjectTeamMemberService : IProjectTeamMemberService
    {
        private readonly IProjectTeamMemberRepository _repo;

        public ProjectTeamMemberService(IProjectTeamMemberRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ProjectTeamMemberDto>> GetByProjectIdAsync(int projectId)
        {
            var members = await _repo.GetAllAsync();
            var filtered = members.Where(m => m.ProjectId == projectId);

            return filtered.Select(m => new ProjectTeamMemberDto
            {
                ProjectId = m.ProjectId,
                UserId = m.UserId
            });
        }

        public async Task AssignAsync(AssignUserToProjectDto dto)
        {
            var member = new ProjectTeamMember
            {
                ProjectId = dto.ProjectId,
                UserId = dto.UserId
            };

            await _repo.AddAsync(member);
            await _repo.SaveChangesAsync();
        }

        public async Task RemoveAsync(int projectId, string userId)
        {
            var member = await _repo.GetByIdAsync(new { projectId, userId });
            _repo.Delete(member);
            await _repo.SaveChangesAsync();
        }
    }
}

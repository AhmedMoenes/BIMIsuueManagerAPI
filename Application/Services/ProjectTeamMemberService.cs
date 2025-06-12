namespace Application.Services
{
    public class ProjectTeamMemberService : IProjectTeamMemberService
    {
        private readonly IProjectTeamMemberRepository _repo;
        public ProjectTeamMemberService(IProjectTeamMemberRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ProjectTeamMemberDto>> GetAllAsync()
        {
            IEnumerable<ProjectTeamMember> members = await _repo.GetAllMembersAsync();
            return members.Select(m => new ProjectTeamMemberDto
            {
                ProjectId = m.ProjectId,
                UserId = m.UserId,
                FullName = m.User != null ? $"{m.User.FirstName} {m.User.LastName}" : string.Empty,
                Email = m.User?.Email,
                ProjectName = m.Project.ProjectName,
                Role = m.Role
            });
        }
        public async Task<IEnumerable<ProjectTeamMemberDto>> GetByProjectIdAsync(int projectId)
        {
            IEnumerable<ProjectTeamMember> members = await _repo.GetByProjectIdAsync(projectId);
            return members.Select(m => new ProjectTeamMemberDto
            {
                ProjectId = m.ProjectId,
                UserId = m.UserId,
                FullName = m.User != null ? $"{m.User.FirstName} {m.User.LastName}" : string.Empty,
                Email = m.User?.Email,
                ProjectName = m.Project.ProjectName,
                Role = m.Role
            });
        }
        public async Task<IEnumerable<ProjectTeamMemberDto>> GetByUserIdAsync(string userId)
        {
            IEnumerable<ProjectTeamMember> memberships = await _repo.GetByUserIdAsync(userId);

            return memberships.Select(pt => new ProjectTeamMemberDto
            {
                ProjectId = pt.ProjectId,
                UserId = pt.UserId,
                FullName = pt.User != null ? $"{pt.User.FirstName} {pt.User.LastName}" : string.Empty,
                Email = pt.User?.Email,
                ProjectName = pt.Project.ProjectName,
                Role = pt.Role
            });
        }
        public async Task<ProjectTeamMemberDto> AssignAsync(AssignUserToProjectDto dto)
        {
            var member = new ProjectTeamMember
            {
                ProjectId = dto.ProjectId,
                UserId = dto.UserId,
                Role = dto.Role
            };

            var created = await _repo.AddAsync(member);

            return new ProjectTeamMemberDto
            {
                ProjectId = created.ProjectId,
                UserId = created.UserId,
                Role = dto.Role
            };
        }
        public async Task<bool> RemoveAsync(int projectId, string userId)
        {
            var member = await _repo.GetByIdAsync(new { projectId, userId });
            if (member == null) return false;

            return await _repo.DeleteAsync(new { projectId, userId });
        }
    }
}

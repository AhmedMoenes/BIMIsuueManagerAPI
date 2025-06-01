using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<ProjectTeamMemberDto>> GetByUserIdAsync(string userId)
        {
            var memberships = await _repo.GetAllAsync();

            var filter = memberships.Where(pt => pt.UserId == userId)
                .Select(pt => new ProjectTeamMemberDto
                {
                    ProjectId = pt.ProjectId,
                    UserId = pt.UserId,
                    Role = pt.Role
                })
                .ToList();
                

            return filter;
        }

    }
}

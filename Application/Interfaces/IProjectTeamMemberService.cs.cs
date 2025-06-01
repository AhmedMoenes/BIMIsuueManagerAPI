using DTOs.ProjectTeamMember;

namespace Application.Interfaces
{
    public interface IProjectTeamMemberService
    {
        Task<IEnumerable<ProjectTeamMemberDto>> GetByProjectIdAsync(int projectId);
        Task<ProjectTeamMemberDto> AssignAsync(AssignUserToProjectDto dto); 
        Task<bool> RemoveAsync(int projectId, string userId);
        Task<IEnumerable<ProjectTeamMemberDto>> GetByUserIdAsync(string userId);

    }
}

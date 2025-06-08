namespace Application.Interfaces
{
    public interface IProjectTeamMemberService
    {
        Task<IEnumerable<ProjectTeamMemberDto>> GetAllAsync();
        Task<IEnumerable<ProjectTeamMemberDto>> GetByProjectIdAsync(int projectId);
        Task<IEnumerable<ProjectTeamMemberDto>> GetByUserIdAsync(string userId);
        Task<ProjectTeamMemberDto> AssignAsync(AssignUserToProjectDto dto); 
        Task<bool> RemoveAsync(int projectId, string userId);

    }
}

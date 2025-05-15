namespace Application.Interfaces
{
    public interface IProjectTeamMemberService
    {
        Task<IEnumerable<ProjectTeamMemberDto>> GetByProjectIdAsync(int projectId);
        Task AssignAsync(AssignUserToProjectDto dto);
        Task RemoveAsync(int projectId, string userId);
    }
}

using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IProjectTeamMemberRepository : IRepository<ProjectTeamMember>
    {
        Task<IEnumerable<ProjectTeamMember>> GetByProjectIdAsync(int projectId);
        Task<IEnumerable<ProjectTeamMember>> GetByUserIdAsync(string userId);
    }
}

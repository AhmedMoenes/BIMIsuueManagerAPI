using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetUsersOverviewAsync();
        Task<User> GetUserOverviewByIdAsync(string userId);
        Task<int> GetCompanyIdAsync(string userId);
        Task AddUserToProjectsAsync(string userId, List<ProjectTeamMember> memberships);
        Task<IEnumerable<User>> GetByProjectIdAsync(int projectId);
        Task<IEnumerable<User>> GetUsersByCompanyAsync(int companyId);
        Task<IEnumerable<User>> GetAllWithDetailsAsync();
    }
}

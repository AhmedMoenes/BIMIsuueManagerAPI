using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<T>> GetUsersOverviewAsync<T>(Func<User, Task<T>> selector);
        Task<User> GetUserOverviewByIdAsync(string userId);
        Task<int> GetCompanyIdAsync(string userId);
        Task AddUserToProjectsAsync(string userId, List<ProjectTeamMember> memberships);
        Task<IEnumerable<User>> GetByProjectIdAsync(int projectId);
    }
}

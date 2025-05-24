using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<T>> GetUserOverviewAsync<T>(Func<User, Task<T>> selector);
        Task<int> GetCompanyIdAsync(string userId);
        Task AddUserToProjectsAsync(string userId, List<ProjectTeamMember> memberships);
    }
}

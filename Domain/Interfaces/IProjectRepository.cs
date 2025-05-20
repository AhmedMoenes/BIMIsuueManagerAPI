using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<IEnumerable<T>> GetProjectOverviewsAsync<T>(Func<Project, Task<T>> selector);
    }
}

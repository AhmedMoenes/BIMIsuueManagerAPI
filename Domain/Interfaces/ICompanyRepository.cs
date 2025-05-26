using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task<IEnumerable<T>> GetUserCompaniesAsync<T>(string userId, Func<Company, Task<T>> selector);
        Task<IEnumerable<Company>> GetAllWithProjectsAsync();

    }
}

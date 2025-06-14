using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICompanyProjectRepository: IRepository<CompanyProject>
    {
        Task AssignCompaniesToProjectAsync(int projectId, List<int> companyIds);
        Task<IEnumerable<Project>> GetByCompanyIdAsync(int companyId);

    }
}

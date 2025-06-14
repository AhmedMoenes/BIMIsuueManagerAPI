
namespace Application.Interfaces
{
    public interface ICompanyProjectService
    {
        Task AssignCompaniesAsync(AssignCompaniesToProjectDto dto);
        Task<IEnumerable<ProjectOverviewDto>> GetForCompanyAsync(int companyId);
    }
}


namespace Application.Interfaces
{
    public interface ICompanyProjectService
    {
        Task AssignCompaniesAsync(AssignCompaniesToProjectDto dto);
    }
}

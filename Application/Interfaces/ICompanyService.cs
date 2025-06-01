namespace Application.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyOverviewDto>> GetAllAsync();
        Task<CompanyDto> GetByIdAsync(int id);
        Task<CompanyDto> CreateAsync(CreateCompanyDto dto);   
        Task<bool> UpdateAsync(int id, UpdateCompanyDto dto); 
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<CompanyOverviewDto>> GetCompaniesForUserAsync(string userId);
        Task<CompanyDto> CreateCompanyWithAdminAsync(CreateCompanyWithAdminDto dto);
    }
}

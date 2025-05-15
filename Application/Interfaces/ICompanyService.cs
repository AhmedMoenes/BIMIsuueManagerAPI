namespace Application.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyDto>> GetAllAsync();
        Task<CompanyDto> GetByIdAsync(int id);
        Task CreateAsync(CreateCompanyDto dto);
        Task UpdateAsync(int id, UpdateCompanyDto dto);
        Task DeleteAsync(int id);
    }
}

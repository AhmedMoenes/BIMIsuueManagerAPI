using Application.DTOs.Company;

namespace Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _repo;

        public CompanyService(ICompanyRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<CompanyDto>> GetAllAsync()
        {
            IEnumerable<Company> companies = await _repo.GetAllAsync();
            return companies.Select(c => new CompanyDto
            {
                CompanyId = c.CompanyId,
                CompanyName = c.CompanyName,
                SubscriberId = c.SubscriberId
            });
        }

        public async Task<CompanyDto> GetByIdAsync(int id)
        {
            Company c = await _repo.GetByIdAsync(id);
            return new CompanyDto
            {
                CompanyId = c.CompanyId,
                CompanyName = c.CompanyName,
                SubscriberId = c.SubscriberId
            };
        }

        public async Task<CompanyDto> CreateAsync(CreateCompanyDto dto)
        {
            var company = new Company
            {
                CompanyName = dto.CompanyName,
                SubscriberId = dto.SubscriberId
            };

            var created = await _repo.AddAsync(company);

            return new CompanyDto
            {
                CompanyId = created.CompanyId,
                CompanyName = created.CompanyName,
                SubscriberId = created.SubscriberId
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateCompanyDto dto)
        {
            var company = await _repo.GetByIdAsync(id);
            if (company == null) return false;

            company.CompanyName = dto.CompanyName;
            company.SubscriberId = dto.SubscriberId;

            return await _repo.UpdateAsync(company);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }
    }
}

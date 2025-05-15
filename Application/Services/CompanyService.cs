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

        public async Task CreateAsync(CreateCompanyDto dto)
        {
            Company company = new Company
            {
                CompanyName = dto.CompanyName,
                SubscriberId = dto.SubscriberId
            };

            await _repo.AddAsync(company);
            await _repo.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, UpdateCompanyDto dto)
        {
            Company company = await _repo.GetByIdAsync(id);
            company.CompanyName = dto.CompanyName;
            company.SubscriberId = dto.SubscriberId;

            _repo.Update(company);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Company company = await _repo.GetByIdAsync(id);
            _repo.Delete(company);
            await _repo.SaveChangesAsync();
        }
    }
}

namespace Infrastructure.Repositories
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<T>> GetUserCompaniesAsync<T>(string userId, Func<Company, Task<T>> selector)
        {
            var userCompanyId = await Context.Users
                .Where(u => u.Id == userId)
                .Select(u => u.CompanyId)
                .FirstOrDefaultAsync();

            if (userCompanyId == 0)
                return Enumerable.Empty<T>();

            var companies = await DbSet
                .Where(c => c.CompanyId == userCompanyId)
                .Include(c => c.Users)
                .Include(c => c.Projects)
                .ThenInclude(p => p.Issues)
                .ToListAsync();

            var result = new List<T>();

            foreach (var company in companies)
            {
                result.Add(await selector(company));
            }

            return result;
        }

        public async Task ExecuteInTransactionAsync(Func<Task> action)
        {
            using var transaction = await Context.Database.BeginTransactionAsync();

            try
            {
                await action();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

    }
}

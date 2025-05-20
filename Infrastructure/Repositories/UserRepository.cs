namespace Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context) { }
        public async Task<IEnumerable<T>> GetUserOverviewAsync<T>(Func<User, Task<T>> selector)
        {
            var users = await DbSet
                .Include(u => u.Company)
                .Include(u => u.ProjectMemberships)
                .Include(u => u.CreatedIssues)
                .ToListAsync();

            var result = new List<T>();

            foreach (var user in users)
            {
                result.Add(await selector(user));
            }

            return result;
        }
        public async Task<int> GetCompanyIdAsync(string userId)
        {
            var companyId = await DbSet
                .Where(u => u.Id == userId)
                .Select(u => u.CompanyId)
                .FirstOrDefaultAsync();

            if (companyId == 0)
                throw new Exception($"Company ID not found for user {userId}");

            return companyId;
        }
    }
}

using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly AppDbContext _context;


        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetUsersOverviewAsync()
        {
            IEnumerable<User> users = await Context.Users
                                      .Include(u => u.Company)
                                      .Include(u => u.ProjectMemberships)
                                      .ThenInclude(pm => pm.Project)
                                      .Include(u => u.CreatedIssues)
                                      .Include(u => u.AssignedIssues)
                                      .ToListAsync();
            return users;
        }

        public async Task<IEnumerable<User>> GetUsersByCompanyAsync(int companyId)
        {
            IEnumerable<User> companyUsers = await _context.Users.Where(u => u.CompanyId == companyId)
                                             .Include(u => u.ProjectMemberships)
                                             .ThenInclude(up => up.Project)
                                             .Include(u => u.CommentsCreated)
                                             .ToListAsync();
            return companyUsers;

        }

        public async Task<User> GetUserOverviewByIdAsync(string userId)
        {
            User user = await Context.Users
                        .Where(u => u.Id == userId)
                        .Include(u => u.Company)
                        .Include(u => u.ProjectMemberships)
                        .ThenInclude(pm => pm.Project)
                        .Include(u => u.CreatedIssues)
                        .ThenInclude(i => i.Project)
                        .Include(u => u.CreatedIssues)
                        .ThenInclude(i => i.AssignedToUser)
                        .Include(u => u.AssignedIssues)
                        .ThenInclude(i => i.Project)
                        .Include(u => u.AssignedIssues)
                        .ThenInclude(i => i.CreatedByUser)
                        .FirstOrDefaultAsync();
            return user;
        }
        public async Task<int> GetCompanyIdAsync(string userId)
        {
            int companyId = await DbSet
                            .Where(u => u.Id == userId)
                            .Select(u => u.CompanyId)
                            .FirstOrDefaultAsync();

            if (companyId == 0)
                throw new Exception($"Company ID not found for user {userId}");

            return companyId;
        }

        public async Task AddUserToProjectsAsync(string userId, List<ProjectTeamMember> memberships)
        {
            foreach (ProjectTeamMember member in memberships)
                member.UserId = userId;

            await _context.ProjectTeamMembers.AddRangeAsync(memberships);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetByProjectIdAsync(int projectId)
        {
            return await Context.Users
                .Where(u => u.ProjectMemberships.Any(pm => pm.ProjectId == projectId))
                .ToListAsync();
        }

        public Task<IEnumerable<User>> GetAllWithDetailsAsync()
        {
            throw new NotImplementedException();
        }
    }
}

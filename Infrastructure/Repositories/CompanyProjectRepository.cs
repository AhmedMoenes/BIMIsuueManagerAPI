using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CompanyProjectRepository: Repository<CompanyProject>, ICompanyProjectRepository
    {
        public CompanyProjectRepository(AppDbContext context) : base(context)
        {

        }

        public async Task AssignCompaniesToProjectAsync(int projectId, List<int> companyIds)
        {
            var existing = Context.CompanyProjects.Where(cp => cp.ProjectId == projectId);
            Context.CompanyProjects.RemoveRange(existing);

            var newLinks = companyIds.Select(id => new CompanyProject
            {
                CompanyId = id,
                ProjectId = projectId
            }).ToList();

            await Context.CompanyProjects.AddRangeAsync(newLinks);
        }
    }
}

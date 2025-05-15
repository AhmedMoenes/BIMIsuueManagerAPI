namespace Application.Services
{
    public class IssueService : IIssueService
    {
        private readonly IIssueRepository _issueRepo;

        public IssueService(IIssueRepository issueRepo)
        {
            _issueRepo = issueRepo;
        }

        public async Task<IEnumerable<IssueDto>> GetAllAsync()
        {
            IEnumerable<Issue> issues = await _issueRepo.GetAllAsync();
            return issues.Select(issue => new IssueDto
            {
                IssueId = issue.IssueId,
                Title = issue.Title,
                Description = issue.Description,
                ProjectId = issue.ProjectId
            });
        }

        public async Task<IssueDto> GeyByIdAsync(int id)
        {
            Issue issue = await _issueRepo.GetByIdAsync(id);
            return new IssueDto()
            {
                IssueId = issue.IssueId,
                Title = issue.Title,
                Description = issue.Description,
                ProjectId = issue.ProjectId
            };
        }

        public async Task CreateAsync(CreateIssueDto dto)
        {
            var issue = new Issue
            {
                Title = dto.Title,
                Description = dto.Description,
                ProjectId = dto.ProjectId,
                CreatedByUserId = dto.CreatedByUserId,
                AssignedToUserId = dto.AssignedToUserId
            };

            await _issueRepo.AddAsync(issue);
            await _issueRepo.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, UpdateIssueDto dto)
        {
            Issue issue = await _issueRepo.GetByIdAsync(id);
            issue.Title = dto.Title;
            issue.Description = dto.Description;
            issue.AssignedToUserId = dto.AssignedToUserId;

            _issueRepo.Update(issue);
            await _issueRepo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Issue issue = await _issueRepo.GetByIdAsync(id);
            _issueRepo.Delete(issue);
            await _issueRepo.SaveChangesAsync();
        }
    }
}

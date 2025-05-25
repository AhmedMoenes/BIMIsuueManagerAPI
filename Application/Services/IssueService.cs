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
                Title = issue.Title,
                Description = issue.Description,
                ProjectId = issue.ProjectId
            });
        }

        public async Task<IssueDto> GetByIdAsync(int id)
        {
            Issue issue = await _issueRepo.GetByIdAsync(id);
            return new IssueDto()
            {
                Title = issue.Title,
                Description = issue.Description,
                ProjectId = issue.ProjectId
            };
        }

        public async Task<IssueDto> CreateAsync(CreateIssueDto dto)
        {
            Issue entity = new Issue
            {
                Title = dto.Title,
                Description = dto.Description,
                AreaId = dto.AreaId,
                Priority = (Domain.Entities.Priority)dto.Priority,
                AssignedToUserId = dto.AssignedToUserId,
                ProjectId = dto.ProjectId,
                CreatedByUserId = dto.CreatedByUserId,
                CreatedAt = DateTime.UtcNow
            };

            var revitElements = dto.RevitElements?.Select(r => new RevitElement
            {
                ElementId = r.ElementId,
                ElementUniqueId = r.ElementUniqueId,
                ViewpointCameraPosition = r.ViewpointCameraPosition,
                SnapshotImagePath = r.SnapshotImagePath
            }).ToList();

            var labels = dto.Labels?.Select(i => new IssueLabel
            {
                LabelId = i.LabelId,
                IssueId = i.IssueId
            }).ToList();

            var created = await _issueRepo.AddAsync(entity);

            return new IssueDto
            {
                Title = created.Title,
                Description = created.Description,
                ProjectId = created.ProjectId,
            };
        }
        public async Task<bool> UpdateAsync(int id, UpdateIssueDto dto)
        {
            var issue = await _issueRepo.GetByIdAsync(id);
            if (issue == null) return false;

            issue.Title = dto.Title;
            issue.Description = dto.Description;
            issue.AssignedToUserId = dto.AssignedToUserId;

            return await _issueRepo.UpdateAsync(issue);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _issueRepo.DeleteAsync(id);
        }
    }
}

using Domain.Interfaces;

namespace Application.Services
{
    public class IssueService : IIssueService
    {
        private readonly IIssueRepository _issueRepoitory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRevitElementRepository _revitElementRepository;
        private readonly IIssueLabelRepository _issueLabelRepository;

        public IssueService(IIssueRepository issueRepository,
                            IUnitOfWork unitOfWork,
                            IRevitElementRepository revitElementRepository,
                            IIssueLabelRepository issueLabelRepository)
        {
            _issueRepoitory = issueRepository;
            _unitOfWork = unitOfWork;
            _revitElementRepository = revitElementRepository;
            _issueLabelRepository = issueLabelRepository;
        }

        public async Task<IEnumerable<IssueDto>> GetAllAsync()
        {
            IEnumerable<Issue> issues = await _issueRepoitory.GetAllAsync();
            return issues.Select(issue => new IssueDto
            {
                Title = issue.Title,
                Description = issue.Description,
                ProjectId = issue.ProjectId
            });
        }
        public async Task<IssueDto> GetByIdAsync(int id)
        {
            Issue issue = await _issueRepoitory.GetByIdAsync(id);
            return new IssueDto()
            {
                Title = issue.Title,
                Description = issue.Description,
                ProjectId = issue.ProjectId
            };
        }

        public async Task<IssueDto> CreateAsync(CreateIssueDto dto)
        {
            await _unitOfWork.BeginTransactionAsync();

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

            Issue created = await _issueRepoitory.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            var labels = dto.Labels?.Select(i => new IssueLabel
            {
                LabelId = i.LabelId,
                IssueId = created.IssueId
            }).ToList();


            if (labels != null && labels.Any())
            {
                await _issueLabelRepository.AddRangeAsync(labels);
            }

            var revitElements = dto.RevitElements?.Select(r => new RevitElement
            {
                IssueId = created.IssueId,
                ElementId = r.ElementId,
                ElementUniqueId = r.ElementUniqueId,
                ViewpointCameraPosition = r.ViewpointCameraPosition,
                SnapshotImagePath = r.SnapshotImagePath
            }).ToList();
            await _revitElementRepository.AddRangeAsync(revitElements);

            return new IssueDto
            {
                Title = created.Title,
                Description = created.Description,
                ProjectId = created.ProjectId,
            };
        }
        public async Task<bool> UpdateAsync(int id, UpdateIssueDto dto)
        {
            var issue = await _issueRepoitory.GetByIdAsync(id);
            if (issue == null) return false;

            issue.Title = dto.Title;
            issue.Description = dto.Description;
            issue.AssignedToUserId = dto.AssignedToUserId;

            return await _issueRepoitory.UpdateAsync(issue);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await _issueRepoitory.DeleteAsync(id);
        }
    }
}

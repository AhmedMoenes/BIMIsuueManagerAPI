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
            IEnumerable<Issue> issues = await _issueRepoitory.GetAllDetailed();
            return issues.Select(issue => new IssueDto
            {
                IssueId = issue.IssueId,
                Title = issue.Title,
                Description = issue.Description,
                Priority = issue.Priority.ToString(),
                ProjectName = issue.Project.ProjectName,
                CreatedAt = issue.CreatedAt,
                CreatedByUser = $"{issue.CreatedByUser.FirstName} {issue.CreatedByUser.LastName}",
                AssignedToUser = issue.AssignedToUser != null
                    ? $"{issue.AssignedToUser.FirstName} {issue.AssignedToUser.LastName}"
                    : null,
                Area = new AreaDto
                {
                    AreaId = issue.Area.AreaId,
                    AreaName = issue.Area.AreaName
                },
                Labels = issue.Labels.Select(l => new LabelDto
                {
                    LabelId = l.Label.LabelId,
                    LabelName = l.Label.LabelName
                }).ToList(),
                Comments = issue.Comments.Select(c => new CommentDto
                {
                    CommentId = c.CommentId,
                    Message = c.Message,
                    CreatedAt = c.CreatedAt,
                    CreatedBy = $"{c.CreatedByUser.FirstName} {c.CreatedByUser.LastName}"
                }).ToList(),
                RevitElements = issue.RevitElements.Select(r => new RevitElementDto
                {
                    ElementId = r.ElementId,
                    ElementUniqueId = r.ElementUniqueId,
                    ViewpointCameraPosition = r.ViewpointCameraPosition,
                    SnapshotImagePath = r.SnapshotImagePath
                }).ToList()
            });
        }
        public async Task<IssueDto> GetByIdAsync(int id)
        {
            Issue issue = await _issueRepoitory.GetByIdDetailed(id);
            if (issue == null) return null;

            return new IssueDto()
            {
                IssueId = issue.IssueId,
                Title = issue.Title,
                Description = issue.Description,
                Priority = issue.Priority.ToString(),
                CreatedAt = issue.CreatedAt,
                CreatedByUser = $"{issue.CreatedByUser.FirstName} {issue.CreatedByUser.LastName}",
                AssignedToUser = issue.AssignedToUser != null
                    ? $"{issue.AssignedToUser.FirstName} {issue.AssignedToUser.LastName}"
                    : null,
                Area = new AreaDto
                {
                    AreaId = issue.Area.AreaId,
                    AreaName = issue.Area.AreaName
                },
                Labels = issue.Labels.Select(l => new LabelDto
                {
                    LabelId = l.Label.LabelId,
                    LabelName = l.Label.LabelName
                }).ToList(),
                Comments = issue.Comments.Select(c => new CommentDto
                {
                    CommentId = c.CommentId,
                    Message = c.Message,
                    CreatedAt = c.CreatedAt,
                    CreatedBy = $"{c.CreatedByUser.FirstName} {c.CreatedByUser.LastName}"
                }).ToList(),
                RevitElements = issue.RevitElements.Select(r => new RevitElementDto
                {
                    ElementId = r.ElementId,
                    ElementUniqueId = r.ElementUniqueId,
                    ViewpointCameraPosition = r.ViewpointCameraPosition,
                    SnapshotImagePath = r.SnapshotImagePath
                }).ToList()
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

            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();

            return new IssueDto
            {
                IssueId = created.IssueId,
                Title = created.Title,
                Description = created.Description,
                Priority = created.Priority.ToString(),
                CreatedByUser = created.CreatedByUserId,
                AssignedToUser = created.AssignedToUserId,
                CreatedAt = created.CreatedAt,
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

        public async Task<IEnumerable<IssueDto>> GetByProjectIdAsync(int projectId)
        {
            var issues = await _issueRepoitory.GetAllDetailed();
            var filteredIssues = issues.Where(i => i.ProjectId == projectId);

            return filteredIssues.Select(issue => new IssueDto
            {
                IssueId = issue.IssueId,
                Title = issue.Title,
                Description = issue.Description,
                Priority = issue.Priority.ToString(),
                ProjectName = issue.Project.ProjectName,
                CreatedAt = issue.CreatedAt,
                CreatedByUser = $"{issue.CreatedByUser.FirstName} {issue.CreatedByUser.LastName}",
                AssignedToUser = issue.AssignedToUser != null
                    ? $"{issue.AssignedToUser.FirstName} {issue.AssignedToUser.LastName}"
                    : null,
                Area = new AreaDto
                {
                    AreaId = issue.Area.AreaId,
                    AreaName = issue.Area.AreaName
                },
                Labels = issue.Labels.Select(l => new LabelDto
                {
                    LabelId = l.Label.LabelId,
                    LabelName = l.Label.LabelName
                }).ToList(),
                Comments = issue.Comments.Select(c => new CommentDto
                {
                    CommentId = c.CommentId,
                    Message = c.Message,
                    CreatedAt = c.CreatedAt,
                    CreatedBy = $"{c.CreatedByUser.FirstName} {c.CreatedByUser.LastName}"
                }).ToList(),
                RevitElements = issue.RevitElements.Select(r => new RevitElementDto
                {
                    ElementId = r.ElementId,
                    ElementUniqueId = r.ElementUniqueId,
                    ViewpointCameraPosition = r.ViewpointCameraPosition,
                    SnapshotImagePath = r.SnapshotImagePath
                }).ToList()
            });
        }

    }
}

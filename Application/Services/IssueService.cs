namespace Application.Services
{
    public class IssueService : IIssueService
    {
        private readonly IIssueRepository _issueRepoitory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRevitElementRepository _revitElementRepository;
        private readonly IIssueLabelRepository _issueLabelRepository;
        private readonly ISnapshotRepository _snapshotRepository;

        public IssueService(IIssueRepository issueRepository,
                            IUnitOfWork unitOfWork,
                            IRevitElementRepository revitElementRepository,
                            IIssueLabelRepository issueLabelRepository,
                            ISnapshotRepository snapshotRepository)
        {
            _issueRepoitory = issueRepository;
            _unitOfWork = unitOfWork;
            _revitElementRepository = revitElementRepository;
            _issueLabelRepository = issueLabelRepository;
            _snapshotRepository = snapshotRepository;
        }

        public async Task<IEnumerable<IssueDto>> GetAllAsync()
        {
            var issues = await _issueRepoitory.GetAllDetailed();
            return issues.Select(issue => ToIssueDto(issue));

        }

        public async Task<IssueDto> GetByIdAsync(int id)
        {
            var issue = await _issueRepoitory.GetByIdDetailed(id);
            return issue == null ? null : ToIssueDto(issue);
        }

        public async Task<IssueDto> CreateAsync(CreateIssueDto dto)
        {
            await _unitOfWork.BeginTransactionAsync();

            var issue = new Issue
            {
                Title = dto.Title,
                Description = dto.Description,
                AreaId = dto.AreaId,
                ProjectId = dto.ProjectId,
                CreatedByUserId = dto.CreatedByUserId,
                AssignedToUserId = dto.AssignedToUserId,
                CreatedAt = DateTime.UtcNow,
                Priority = (Domain.Entities.Priority)dto.Priority,
                IsResolved = dto.IsResolved,
                IsDeleted = false
            };

            var created = await _issueRepoitory.AddAsync(issue);
            await _unitOfWork.SaveChangesAsync();

            if (dto.Labels?.Any() == true)
            {
                var labels = dto.Labels.Select(l => new IssueLabel
                {
                    IssueId = created.IssueId,
                    LabelId = l.LabelId
                }).ToList();
                await _issueLabelRepository.AddRangeAsync(labels);
            }

            if (dto.RevitElements?.Any() == true)
            {
                var elements = dto.RevitElements.Select(r => new RevitElement
                {
                    IssueId = created.IssueId,
                    ElementId = r.ElementId,
                    ElementUniqueId = r.ElementUniqueId,
                    ViewpointCameraPosition = r.ViewpointCameraPosition,
                    ViewpointForwardDirection = r.ViewpointForwardDirection,
                    ViewpointUpDirection = r.ViewpointUpDirection
                }).ToList();
                await _revitElementRepository.AddRangeAsync(elements);
            }

            Snapshot? snapshot = null;
            if (dto.Snapshot is not null)
            {
                snapshot = new Snapshot
                {
                    IssueId = created.IssueId,
                    Path = dto.Snapshot.Path,
                    CreatedAt = dto.Snapshot.CreatedAt
                };
                await _snapshotRepository.AddAsync(snapshot);
            }

            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();

            var full = await _issueRepoitory.GetByIdDetailed(created.IssueId);
            return ToIssueDto(full, snapshot);
        }

        public async Task<bool> UpdateAsync(int id, UpdateIssueDto dto)
        {
            var issue = await _issueRepoitory.GetByIdDetailed(id);
            if (issue == null || issue.IsDeleted) return false;

            issue.Title = dto.Title;
            issue.Description = dto.Description;
            issue.AreaId = dto.AreaId;
            issue.AssignedToUserId = dto.AssignedToUserId;
            issue.Priority = (Domain.Entities.Priority)dto.Priority;
            issue.IsResolved = dto.IsResolved;
            issue.IsDeleted = dto.IsDeleted;

            if (issue.Labels is not null && issue.Labels.Any())
                await _issueLabelRepository.DeleteByIssueIdAsync(id);

            if (dto.Labels?.Any() == true)
            {
                var newLabels = dto.Labels.Select(l => new IssueLabel
                {
                    IssueId = id,
                    LabelId = l.LabelId
                }).ToList();

                await _issueLabelRepository.AddRangeAsync(newLabels);
            }

            return await _issueRepoitory.UpdateAsync(issue);
        }


        public async Task<bool> DeleteAsync(int id)
        {
            return await _issueRepoitory.DeleteAsync(id);
        }

        public async Task<bool> MarkAsResolvedAsync(int id)
        {
            var issue = await _issueRepoitory.GetByIdAsync(id);
            if (issue == null || issue.IsDeleted) return false;

            issue.IsResolved = true;
            return await _issueRepoitory.UpdateAsync(issue);
        }

        public async Task<bool> RestoreAsync(int id)
        {
            var issue = await _issueRepoitory.GetByIdAsync(id);
            if (issue == null || !issue.IsDeleted) return false;

            issue.IsDeleted = false;
            return await _issueRepoitory.UpdateAsync(issue);
        }

        public async Task<IEnumerable<IssueDto>> GetResolvedIssuesAsync()
        {
            var issues = await _issueRepoitory.GetAllDetailed();
            return issues
                .Where(i => i.IsResolved && !i.IsDeleted)
                .Select(issue => ToIssueDto(issue));

        }

        public async Task<IEnumerable<IssueDto>> GetUnresolvedIssuesAsync()
        {
            var issues = await _issueRepoitory.GetAllDetailed();
            return issues
                .Where(i => !i.IsResolved && !i.IsDeleted)
                .Select(issue => ToIssueDto(issue));
        }

        public async Task<IEnumerable<IssueDto>> GetDeletedIssuesAsync()
        {
            var issues = await _issueRepoitory.GetAllDetailed();
            return issues
                .Where(i => i.IsDeleted)
                .Select(issue => ToIssueDto(issue));

        }

        public async Task<IEnumerable<IssueDto>> GetByProjectIdAsync(int projectId)
        {
            var issues = await _issueRepoitory.GetByProjectIdDetailedAsync(projectId);
            return issues.Select(issue => ToIssueDto(issue));

        }

        public async Task<IEnumerable<IssueDto>> GetByUserIdAsync(string userId)
        {
            var issues = await _issueRepoitory.GetByUserIdDetailedAsync(userId);
            return issues.Select(issue => ToIssueDto(issue));

        }

        private static IssueDto ToIssueDto(Issue issue, Snapshot? singleSnapshot = null)
        {
            var snapshot = singleSnapshot ?? issue.Snapshots?.FirstOrDefault();

            return new IssueDto
            {
                IssueId = issue.IssueId,
                Title = issue.Title,
                Description = issue.Description,
                CreatedAt = issue.CreatedAt,
                Priority = (DTOs.Issues.Priority)issue.Priority,
                IsResolved = issue.IsResolved,
                IsDeleted = issue.IsDeleted,
                ProjectName = issue.Project?.ProjectName ?? "",
                CreatedByUser = $"{issue.CreatedByUser?.FirstName} {issue.CreatedByUser?.LastName}",
                AssignedToUser = issue.AssignedToUser != null
                    ? $"{issue.AssignedToUser.FirstName} {issue.AssignedToUser.LastName}"
                    : null,
                AssignedToUserId = issue.AssignedToUserId,
                Area = new AreaDto
                {
                    AreaId = issue.Area.AreaId,
                    AreaName = issue.Area.AreaName
                },
                Labels = issue.Labels?.Select(l => new LabelDto
                {
                    LabelId = l.Label.LabelId,
                    LabelName = l.Label.LabelName
                }).ToList() ?? new(),
                Comments = issue.Comments?.Select(c => new CommentDto
                {
                    CommentId = c.CommentId,
                    Message = c.Message,
                    CreatedAt = c.CreatedAt,
                    CreatedBy = $"{c.CreatedByUser?.FirstName} {c.CreatedByUser?.LastName}",
                    SnapshotId = c.SnapshotId
                }).ToList() ?? new(),
                RevitElements = issue.RevitElements?.Select(r => new RevitElementDto
                {
                    ElementId = r.ElementId,
                    ElementUniqueId = r.ElementUniqueId,
                    ViewpointCameraPosition = r.ViewpointCameraPosition,
                    ViewpointForwardDirection = r.ViewpointForwardDirection,
                    ViewpointUpDirection = r.ViewpointUpDirection
                }).ToList() ?? new(),
                Snapshot = snapshot != null
                    ? new SnapshotDto
                    {
                        Path = snapshot.Path,
                        CreatedAt = snapshot.CreatedAt
                    }
                    : null
            };
        }
    }
}

namespace Application.Services
{
    public class RevitElementService : IRevitElementService
    {
        private readonly IRevitElementRepository _repo;

        public RevitElementService(IRevitElementRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<RevitElementDto>> GetByIssueIdAsync(int issueId)
        {
            var elements = await _repo.GetAllAsync();
            var filtered = elements.Where(e => e.IssueId == issueId);

            return filtered.Select(e => new RevitElementDto
            {
                RevitElementId = e.RevitElementId,
                ElementId = e.ElementId,
                ElementUniqueId = e.ElementUniqueId,
                ViewpointCameraPosition = e.ViewpointCameraPosition,
                ViewpointForwardDirection = e.ViewpointForwardDirection,
                ViewpointUpDirection = e.ViewpointUpDirection,
                IssueId = e.IssueId
            });
        }

        public async Task<RevitElementDto> AddAsync(RevitElementDto dto)
        {
            var element = new RevitElement
            {
                ElementId = dto.ElementId,
                ElementUniqueId = dto.ElementUniqueId,
                ViewpointCameraPosition = dto.ViewpointCameraPosition,
                ViewpointForwardDirection = dto.ViewpointForwardDirection,
                ViewpointUpDirection = dto.ViewpointUpDirection,
                IssueId = dto.IssueId
            };

            var created = await _repo.AddAsync(element);

            return new RevitElementDto
            {
                RevitElementId = created.RevitElementId,
                ElementId = created.ElementId,
                ElementUniqueId = created.ElementUniqueId,
                ViewpointCameraPosition = created.ViewpointCameraPosition,
                ViewpointForwardDirection = created.ViewpointForwardDirection,
                ViewpointUpDirection = created.ViewpointUpDirection,
                IssueId = created.IssueId
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }
    }
}

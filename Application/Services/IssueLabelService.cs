namespace Application.Services
{
    public class IssueLabelService : IIssueLabelService
    {
        private readonly IIssueLabelRepository _repo;

        public IssueLabelService(IIssueLabelRepository repo)
        {
            _repo = repo;
        }

        public async Task AssignLabelToIssueAsync(AssignLabelToIssueDto dto)
        {
            var link = new IssueLabel
            {
                IssueId = dto.IssueId,
                LabelId = dto.LabelId
            };

            await _repo.AddAsync(link);
            await _repo.SaveChangesAsync();
        }

        public async Task RemoveLabelFromIssueAsync(int issueId, int labelId)
        {
            IssueLabel link = await _repo.GetByIdAsync(new { issueId, labelId });
            _repo.Delete(link);
            await _repo.SaveChangesAsync();
        }
    }
}

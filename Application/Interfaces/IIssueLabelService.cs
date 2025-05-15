namespace Application.Interfaces
{
    public interface IIssueLabelService
    {
        Task AssignLabelToIssueAsync(AssignLabelToIssueDto dto);
        Task RemoveLabelFromIssueAsync(int issueId, int labelId);
    }
}

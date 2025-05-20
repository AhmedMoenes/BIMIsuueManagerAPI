using Application.DTOs.Issues;

namespace Application.Interfaces
{
    public interface IIssueLabelService
    {
        Task<AssignLabelToIssueDto> AssignLabelToIssueAsync(AssignLabelToIssueDto dto); 
        Task<bool> RemoveLabelFromIssueAsync(int issueId, int labelId);         
    }
}

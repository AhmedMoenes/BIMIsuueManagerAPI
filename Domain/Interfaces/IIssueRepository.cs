using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IIssueRepository : IRepository<Issue>
    {
        Task <Issue> CreateDetailedAsync<T>(Issue issue,
            List<Comment> comments,
            List<RevitElement> revitElements,
            List<IssueLabel> issueLabels);
    }
}

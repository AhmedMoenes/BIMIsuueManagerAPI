﻿using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<IEnumerable<T>> GetProjectOverviewsAsync<T>(Func<Project, Task<T>> selector);
        Task<Project> CreateDetailedAsync<T>(Project project,
            List<Area> areas,
            List<Label> labels,
            List<ProjectTeamMember> teamMembers);
        Task<IEnumerable<Project>> GetAllWithCompaniesAsync();
        Task<IEnumerable<Project>> GetByUserIdAsync(string userId);
        Task<Project> GetByIssueIdAsync(int issueId);
    }
}

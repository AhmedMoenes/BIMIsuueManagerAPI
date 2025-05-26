using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ILabelRepository : IRepository<Label>
    {
        Task<IEnumerable<Label>> GetByProjectIdAsync(int projectId);
    }
}

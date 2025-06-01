using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAreaRepository : IRepository<Area>
    {
        Task<IEnumerable<Area>> GetByProjectIdAsync(int projectId);

    }
}

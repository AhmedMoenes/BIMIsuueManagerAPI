
namespace Application.Interfaces
{
    public interface IAreaService
    {
        Task<IEnumerable<AreaDto>> GetAllAsync();
        Task<AreaDto> GetByIdAsync(int id);
        Task CreateAsync(AreaDto dto);
        Task UpdateAsync(int id, AreaDto dto);
        Task DeleteAsync(int id);
    }
}

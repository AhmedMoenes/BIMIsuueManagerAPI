
namespace Application.Interfaces
{
    public interface IAreaService
    {
        Task<IEnumerable<AreaDto>> GetAllAsync();
        Task<AreaDto> GetByIdAsync(int id);
        Task<AreaDto> CreateAsync(CreateAreaDto dto);
        Task<bool> UpdateAsync(int id, AreaDto dto);
        Task<bool> DeleteAsync(int id);
    }
}

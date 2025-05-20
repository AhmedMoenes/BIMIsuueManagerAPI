
using Application.DTOs.Areas;

namespace Application.Interfaces
{
    public interface IAreaService
    {
        Task<IEnumerable<AreaDto>> GetAllAsync();
        Task<AreaDto> GetByIdAsync(int id);
        Task<AreaDto> CreateAsync(AreaDto dto);
        Task<bool> UpdateAsync(int id, AreaDto dto);
        Task<bool> DeleteAsync(int id);
    }
}

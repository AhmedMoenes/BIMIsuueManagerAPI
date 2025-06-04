using DTOs.Snapshots;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
    public interface ISnapshotService
    {
        Task<IEnumerable<SnapshotDto>> GetAllAsync();
        Task<SnapshotDto> GetByIdAsync(int id);
        Task<SnapshotDto> CreateAsync(SnapshotDto dto);
        Task<bool> UpdateAsync(int id, SnapshotDto dto);
        Task<bool> DeleteAsync(int id);
        Task<string> UploadImageAsync(IFormFile file);
        Task<SnapshotDto> DownloadImageAsync();
    }
}

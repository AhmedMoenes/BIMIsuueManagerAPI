using DTOs.Snapshots;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class SnapshotService : ISnapshotService
    {
        private readonly ISnapshotRepository _snapshotRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _environment;

        public SnapshotService(ISnapshotRepository snapshotRepo,
            IUnitOfWork unitOfWork,
            IWebHostEnvironment environment)
        {
            _snapshotRepo = snapshotRepo;
            _unitOfWork = unitOfWork;
            _environment = environment;
        }

        public async Task<IEnumerable<SnapshotDto>> GetAllAsync()
        {
            IEnumerable<Snapshot> snapshots = await _snapshotRepo.GetAllAsync();
            return snapshots.Select(snapshot => new SnapshotDto
            {
                Path = snapshot.Path,
                CreatedAt = snapshot.CreatedAt
            });
        }

        public async Task<SnapshotDto> GetByIdAsync(int id)
        {
            Snapshot snapshot = await _snapshotRepo.GetByIdAsync(id);
            if (snapshot == null) return null;

            return new SnapshotDto
            {
                Path = snapshot.Path,
                CreatedAt = snapshot.CreatedAt
            };
        }

        public async Task<SnapshotDto> CreateAsync(SnapshotDto dto)
        {
            Snapshot snapshot = new Snapshot
            {
                Path = dto.Path,
                CreatedAt = dto.CreatedAt
            };

            Snapshot created = await _snapshotRepo.AddAsync(snapshot);

            return new SnapshotDto
            {
                Path = created.Path,
                CreatedAt = created.CreatedAt
            };
        }

        public async Task<bool> UpdateAsync(int id, SnapshotDto dto)
        {
            Snapshot snapshot = await _snapshotRepo.GetByIdAsync(id);
            if (snapshot == null) return false;

            snapshot.Path = dto.Path;
            snapshot.CreatedAt = dto.CreatedAt;

            return await _snapshotRepo.UpdateAsync(snapshot);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _snapshotRepo.DeleteAsync(id);
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "snapshots");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Path.Combine("snapshots", fileName).Replace("\\", "/");
        }

        public async Task<SnapshotDto> DownloadImageAsync()
        {
            throw new NotImplementedException();
        }
    }
}

using DTOs.Snapshots;

namespace Application.Services
{
    public class SnapshotService : ISnapshotService
    {
        private readonly ISnapshotRepository _snapshotRepo;

        public SnapshotService(ISnapshotRepository snapshotRepo)
        {
            _snapshotRepo = snapshotRepo;
        }

        public async Task<IEnumerable<SnapshotDto>> GetAllAsync()
        {
            var snapshots = await _snapshotRepo.GetAllAsync();
            return snapshots.Select(snapshot => new SnapshotDto
            {
                Path = snapshot.Path,
                CreatedAt = snapshot.CreatedAt
            });
        }

        public async Task<SnapshotDto> GetByIdAsync(int id)
        {
            var snapshot = await _snapshotRepo.GetByIdAsync(id);
            if (snapshot == null) return null;

            return new SnapshotDto
            {
                Path = snapshot.Path,
                CreatedAt = snapshot.CreatedAt
            };
        }

        public async Task<SnapshotDto> CreateAsync(SnapshotDto dto)
        {
            var snapshot = new Snapshot
            {
                Path = dto.Path,
                CreatedAt = dto.CreatedAt
            };

            var created = await _snapshotRepo.AddAsync(snapshot);

            return new SnapshotDto
            {
                Path = created.Path,
                CreatedAt = created.CreatedAt
            };
        }

        public async Task<bool> UpdateAsync(int id, SnapshotDto dto)
        {
            var snapshot = await _snapshotRepo.GetByIdAsync(id);
            if (snapshot == null) return false;

            snapshot.Path = dto.Path;
            snapshot.CreatedAt = dto.CreatedAt;

            return await _snapshotRepo.UpdateAsync(snapshot);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _snapshotRepo.DeleteAsync(id);
        }
    }
}

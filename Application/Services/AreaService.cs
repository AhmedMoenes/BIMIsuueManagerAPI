using Domain.Entities;

namespace Application.Services
{
    public class AreaService : IAreaService
    {
        private readonly IAreaRepository _areaRepo;

        public AreaService(IAreaRepository areaRepo)
        {
            _areaRepo = areaRepo;
        }

        public async Task<IEnumerable<AreaDto>> GetAllAsync()
        {
            IEnumerable<Area> areas = await _areaRepo.GetAllAsync();
            return areas.Select(area => new AreaDto
            {
                AreaId = area.AreaId,
                AreaName = area.AreaName
            });
        }

        public async Task<AreaDto> GetByIdAsync(int id)
        {
            Area area = await _areaRepo.GetByIdAsync(id);
            return new AreaDto()
            {
                AreaId = area.AreaId,
                AreaName = area.AreaName
            };
        }

        public async Task CreateAsync(AreaDto dto)
        {
            var area = new Area
            {
                AreaId = dto.AreaId,
                AreaName = dto.AreaName
            };
            await _areaRepo.AddAsync(area);
            await _areaRepo.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, AreaDto dto)
        {
            Area area = await _areaRepo.GetByIdAsync(id);
            area.AreaId = dto.AreaId;
            area.AreaName = dto.AreaName;

            _areaRepo.Update(area);
            await _areaRepo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Area area = await _areaRepo.GetByIdAsync(id);
            _areaRepo.Delete(area);
            await _areaRepo.SaveChangesAsync();
        }
    }
}

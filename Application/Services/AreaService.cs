using Application.DTOs.Area;
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

        public async Task<AreaDto> CreateAsync(AreaDto dto)
        {
            var area = new Area
            {
                AreaName = dto.AreaName
            };

            var created = await _areaRepo.AddAsync(area);

            return new AreaDto
            {
                AreaId = created.AreaId,
                AreaName = created.AreaName
            };
        }

        public async Task<bool> UpdateAsync(int id, AreaDto dto)
        {
            var area = await _areaRepo.GetByIdAsync(id);
            if (area == null) return false;

            area.AreaName = dto.AreaName;

            return await _areaRepo.UpdateAsync(area);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _areaRepo.DeleteAsync(id);
        }
    }
}

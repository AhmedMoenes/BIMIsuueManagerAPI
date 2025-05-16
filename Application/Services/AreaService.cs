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
            throw new NotImplementedException();
        }

        public async Task<AreaDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(AreaDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(int id, AreaDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}

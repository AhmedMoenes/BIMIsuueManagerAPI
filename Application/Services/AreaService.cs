namespace Application.Services
{
    public class AreaService
    {
        private readonly IAreaRepository _areaRepo;

        public AreaService(IAreaRepository areaRepo)
        {
            _areaRepo = areaRepo;
        }

    }
}

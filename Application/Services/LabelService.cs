namespace Application.Services
{
    public class LabelService : ILabelService
    {
        private readonly ILabelRepository _repo;

        public LabelService(ILabelRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<LabelDto>> GetAllAsync()
        {
            IEnumerable<Label> labels = await _repo.GetAllAsync();
            return labels.Select(l => new LabelDto
            {
                LabelId = l.LabelId,
                LabelName = l.LabelName
            });
        }

        public async Task<LabelDto> GetByIdAsync(int id)
        {
            Label l = await _repo.GetByIdAsync(id);
            return new LabelDto
            {
                LabelId = l.LabelId,
                LabelName = l.LabelName
            };
        }

        public async Task CreateAsync(CreateLabelDto dto)
        {
            Label label = new Label
            {
                LabelName = dto.LabelName
            };

            await _repo.AddAsync(label);
            await _repo.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, UpdateLabelDto dto)
        {
            Label label = await _repo.GetByIdAsync(id);
            label.LabelName = dto.LabelName;

            _repo.Update(label);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Label label = await _repo.GetByIdAsync(id);
            _repo.Delete(label);
            await _repo.SaveChangesAsync();
        }
    }
}

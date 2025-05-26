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

        public async Task<LabelDto> CreateAsync(CreateLabelDto dto)
        {
            var label = new Label
            {
                LabelName = dto.LabelName
            };

            var created = await _repo.AddAsync(label);

            return new LabelDto
            {
                LabelId = created.LabelId,
                LabelName = created.LabelName
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateLabelDto dto)
        {
            var label = await _repo.GetByIdAsync(id);
            if (label == null) return false;

            label.LabelName = dto.LabelName;

            return await _repo.UpdateAsync(label);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }

        public async Task<IEnumerable<LabelDto>> GetByProjectIdAsync(int projectId)
        {
            IEnumerable<Label> labels = await _repo.GetByProjectIdAsync(projectId);

            return labels.Select(l => new LabelDto
            {
                LabelId = l.LabelId,
                LabelName = l.LabelName
            });
        }
    }
}

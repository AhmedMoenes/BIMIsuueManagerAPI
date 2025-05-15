namespace Application.Interfaces
{
    public interface ILabelService
    {
        Task<IEnumerable<LabelDto>> GetAllAsync();
        Task<LabelDto> GetByIdAsync(int id);
        Task CreateAsync(CreateLabelDto dto);
        Task UpdateAsync(int id, UpdateLabelDto dto);
        Task DeleteAsync(int id);
    }
}

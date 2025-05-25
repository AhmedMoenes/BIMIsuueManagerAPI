namespace Application.Interfaces
{
    public interface ILabelService
    {
        Task<IEnumerable<LabelDto>> GetAllAsync();
        Task<LabelDto> GetByIdAsync(int id);
        Task<LabelDto> CreateAsync(CreateLabelDto dto);    
        Task<bool> UpdateAsync(int id, UpdateLabelDto dto);
        Task<bool> DeleteAsync(int id);                    
    }
}

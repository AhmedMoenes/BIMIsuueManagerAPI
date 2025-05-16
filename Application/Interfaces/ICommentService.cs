
namespace Application.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDto>> GetAllAsync();
        Task<CommentDto> GetByIdAsync(int id);
        Task CreateAsync(CommentDto dto);
        Task UpdateAsync(int id, CommentDto dto);
        Task DeleteAsync(int id);
    }
}

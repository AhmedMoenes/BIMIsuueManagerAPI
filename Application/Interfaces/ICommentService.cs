
using Application.DTOs.Comment;

namespace Application.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDto>> GetAllAsync();
        Task<CommentDto> GetByIdAsync(int id);
        Task <CommentDto> CreateAsync(CommentDto dto);
        Task<bool> UpdateAsync(int id, CommentDto dto);
        Task<bool> DeleteAsync(int id);
    }
}

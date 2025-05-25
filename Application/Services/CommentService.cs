namespace Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepo;

        public CommentService(ICommentRepository commentRepo)
        {
            _commentRepo = commentRepo;
        }

        public async Task<IEnumerable<CommentDto>> GetAllAsync()
        {
            IEnumerable<Comment> comments = await _commentRepo.GetAllAsync();
            return comments.Select(comment => new CommentDto
            {
                CommentId = comment.CommentId,
                Content = comment.Message
            });
        }

        public async Task<CommentDto> GetByIdAsync(int id)
        {
            Comment comment = await _commentRepo.GetByIdAsync(id);
            return new CommentDto()
            {
                CommentId = comment.CommentId,
                Content = comment.Message
            };
        }

        public async Task<CommentDto> CreateAsync(CommentDto dto)
        {
            var comment = new Comment
            {
                Message = dto.Content
            };

            var created = await _commentRepo.AddAsync(comment);

            return new CommentDto
            {
                CommentId = created.CommentId,
                Content = created.Message
            };
        }

        public async Task<bool> UpdateAsync(int id, CommentDto dto)
        {
            var comment = await _commentRepo.GetByIdAsync(id);
            if (comment == null) return false;

            comment.Message = dto.Content;

            return await _commentRepo.UpdateAsync(comment);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _commentRepo.DeleteAsync(id);
        }
    }
}

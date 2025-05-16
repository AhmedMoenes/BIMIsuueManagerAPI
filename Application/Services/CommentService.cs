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
                Content = comment.Content
            });
        }

        public async Task<CommentDto> GetByIdAsync(int id)
        {
            Comment comment = await _commentRepo.GetByIdAsync(id);
            return new CommentDto()
            {
                CommentId = comment.CommentId,
                Content = comment.Content
            };
        }

        public async Task CreateAsync(CommentDto dto)
        {
            var comment = new Comment
            {
                CommentId = dto.CommentId,
                Content = dto.Content
            };
            await _commentRepo.AddAsync(comment);
            await _commentRepo.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, CommentDto dto)
        {
            Comment comment = await _commentRepo.GetByIdAsync(id);
            comment.CommentId = dto.CommentId;
            comment.Content = dto.Content;

            _commentRepo.Update(comment);
            await _commentRepo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Comment comment = await _commentRepo.GetByIdAsync(id);
            _commentRepo.Delete(comment);
            await _commentRepo.SaveChangesAsync();
        }
    }
}

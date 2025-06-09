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
                Message = comment.Message,
                SnapshotId = comment.SnapshotId
            });
        }
        public async Task<CommentDto> GetByIdAsync(int id)
        {
            Comment comment = await _commentRepo.GetByIdAsync(id);
            return new CommentDto()
            {
                CommentId = comment.CommentId,
                Message = comment.Message,
                SnapshotId = comment.SnapshotId
            };
        }
        public async Task<CommentDto> CreateAsync(CreateCommentDto dto)
        {
            Comment comment = new Comment
            {
                Message = dto.Message,
                IssueId = dto.IssueId,
                SnapshotId = dto.SnapshotId,
                CreatedByUserId = dto.CreatedByUserId,
                CreatedAt = DateTime.UtcNow
            };

            Comment created = await _commentRepo.AddAsync(comment);
            await _commentRepo.SaveChangesAsync();

            return new CommentDto
            {
                Message = created.Message,
                CreatedAt = created.CreatedAt,
                CreatedByUserId = created.CreatedByUserId,
                SnapshotId = created.SnapshotId
            };
        }
        public async Task<IEnumerable<CommentDto>> GetByIssueIdAsync(int issueId)
        {
            IEnumerable<Comment> comments = await _commentRepo.GetByIssueIdAsync(issueId);
            return comments.Select(c => new CommentDto
            {
                CommentId = c.CommentId,
                Message = c.Message,
                CreatedAt = c.CreatedAt,
                CreatedByUserId = c.CreatedByUserId,
                SnapshotId = c.SnapshotId
            }); ;
        }
        public async Task<IEnumerable<CommentDto>> GetBySnapshotIdAsync(int snapshotId)
        {
            IEnumerable<Comment> comments = await _commentRepo.GetBySnapshotIdAsync(snapshotId);
            return comments.Select(c => new CommentDto
            {
                CommentId = c.CommentId,
                Message = c.Message,
                CreatedAt = c.CreatedAt,
                CreatedByUserId = c.CreatedByUserId,
                SnapshotId = c.SnapshotId
            }); ;
        }
        public async Task<bool> UpdateAsync(int id, CommentDto dto)
        {
            var comment = await _commentRepo.GetByIdAsync(id);
            if (comment == null) return false;

            comment.Message = dto.Message;

            return await _commentRepo.UpdateAsync(comment);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await _commentRepo.DeleteAsync(id);
        }
    }
}

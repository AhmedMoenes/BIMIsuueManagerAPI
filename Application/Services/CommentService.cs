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
            throw new NotImplementedException();
        }

        public async Task<CommentDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(CommentDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(int id, CommentDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}

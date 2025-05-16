namespace Application.Services
{
    public class CommentService : 
    {
        private readonly ICommentRepository _commentRepo;

        public CommentService(ICommentRepository commentRepo)
        {
            _commentRepo = commentRepo;
        }
    }
}

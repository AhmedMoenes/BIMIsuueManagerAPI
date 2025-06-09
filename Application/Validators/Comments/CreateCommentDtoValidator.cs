namespace Application.Validators.Comments
{
    public class CreateCommentDtoValidator : AbstractValidator<CreateCommentDto>
    {
        public CreateCommentDtoValidator()
        {
            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Comment can't be empty");

            RuleFor(x => x.IssueId)
                .NotEmpty().GreaterThan(1)
                .WithMessage("Issue ID can't be zero or negative.");

            RuleFor(x => x.CreatedByUserId)
                .NotEmpty().WithMessage("CreatedByUserId is required.");
        }
    }
}

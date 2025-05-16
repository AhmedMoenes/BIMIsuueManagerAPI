namespace Application.Validators
{
    public class CreateCommentDtoValidator : AbstractValidator<Comment>
    {
        public CreateCommentDtoValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty();

            RuleFor(x => x.IssueId)
                .NotEmpty().GreaterThan(1)
                .WithMessage("Issue ID can't be zero or negative.");
        }
    }
}

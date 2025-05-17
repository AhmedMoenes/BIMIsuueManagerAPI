namespace Application.Validators
{
    public class UpdateCommentDtoValidator : AbstractValidator<Comment>
    {
        public UpdateCommentDtoValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Comment can not be left empty.");

        }
    }
}

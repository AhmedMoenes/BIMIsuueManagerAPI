namespace Application.Validators
{
    public class UpdateAreaDtoValidator : AbstractValidator<UpdateAreaDto>
    {
        public UpdateAreaDtoValidator()
        {
            RuleFor(x => x.AreaName)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}

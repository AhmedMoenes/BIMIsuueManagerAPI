namespace Application.Validators
{
    public class UpdateAreaDtoValidator : AbstractValidator<Area>
    {
        public UpdateAreaDtoValidator()
        {
            RuleFor(x => x.AreaName)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}

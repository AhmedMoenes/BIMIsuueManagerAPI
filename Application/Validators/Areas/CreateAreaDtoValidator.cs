namespace Application.Validators.Areas
{
    public class CreateAreaDtoValidator : AbstractValidator<AreaDto>
    {
        public CreateAreaDtoValidator()
        {
            RuleFor(x => x.AreaName)
            .NotEmpty()
            .MaximumLength(100);
        }
    }
}

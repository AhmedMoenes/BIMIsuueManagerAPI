namespace Application.Validators.Areas
{
    public class CreateAreaDtoValidator : AbstractValidator<CreateAreaDto>
    {
        public CreateAreaDtoValidator()
        {
            RuleFor(x => x.AreaName)
            .NotEmpty()
            .MaximumLength(100);
        }
    }
}

namespace Application.Validators.Labels
{
    public class CreateLabelDtoValidator : AbstractValidator<CreateLabelDto>
    {
        public CreateLabelDtoValidator()
        {
            RuleFor(x => x.LabelName)
            .NotEmpty()
            .MaximumLength(100);
        }
    }
}

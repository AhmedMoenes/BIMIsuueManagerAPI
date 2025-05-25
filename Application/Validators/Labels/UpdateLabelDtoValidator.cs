namespace Application.Validators.Labels
{
    public class UpdateLabelDtoValidator : AbstractValidator<UpdateLabelDto>
    {
        public UpdateLabelDtoValidator()
        {
            RuleFor(x => x.LabelName)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}

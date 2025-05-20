using Application.DTOs.Labels;

namespace Application.Validators
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

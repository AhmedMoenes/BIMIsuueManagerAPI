using Application.DTOs.Label;

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

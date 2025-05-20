using Application.DTOs.Label;

namespace Application.Validators
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

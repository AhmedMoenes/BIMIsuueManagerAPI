using Application.DTOs.Issue;

namespace Application.Validators
{
    public class UpdateIssueDtoValidator : AbstractValidator<UpdateIssueDto>
    {
        public UpdateIssueDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Description)
                .MaximumLength(1000);
        }
    }
}

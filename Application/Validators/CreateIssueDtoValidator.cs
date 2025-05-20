using Application.DTOs.Issue;

namespace Application.Validators
{
    public class CreateIssueDtoValidator : AbstractValidator<CreateIssueDto>
    {
        public CreateIssueDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required")
                .MaximumLength(200);

            RuleFor(x => x.Description)
                .MaximumLength(1000);

            RuleFor(x => x.ProjectId)
                .GreaterThan(0)
                .WithMessage("ProjectId must be greater than 0");

            RuleFor(x => x.CreatedByUserId)
                .NotEmpty()
                .WithMessage("CreatedByUserId is required");
        }
    }
}

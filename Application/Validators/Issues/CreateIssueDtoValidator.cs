namespace Application.Validators.Issues
{
    public class CreateIssueDtoValidator : AbstractValidator<CreateIssueDto>
    {
        public CreateIssueDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MinimumLength(3).WithMessage("Title must be at least 3 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Description));

            RuleFor(x => x.AreaId)
                .GreaterThan(0).WithMessage("AreaId must be greater than 0.");

            RuleFor(x => x.ProjectId)
                .GreaterThan(0).WithMessage("ProjectId must be greater than 0.");

            RuleFor(x => x.CreatedByUserId)
                .NotEmpty().WithMessage("CreatedByUserId is required.");

            RuleFor(x => x.Priority)
                .IsInEnum().WithMessage("Invalid priority value.");

            RuleFor(x => x.IsResolved)
                .Equal(false).WithMessage("Issue cannot be marked as resolved on creation.");
        }
    }
}

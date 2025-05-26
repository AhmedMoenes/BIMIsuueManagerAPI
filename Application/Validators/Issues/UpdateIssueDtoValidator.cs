namespace Application.Validators
{
    public class UpdateIssueDtoValidator : AbstractValidator<UpdateIssueDto>
    {
        public UpdateIssueDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MinimumLength(3).WithMessage("Title must be at least 3 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Description));

            RuleFor(x => x.AreaId)
                .GreaterThan(0).WithMessage("AreaId must be greater than 0.");

            RuleFor(x => x.Priority)
                .IsInEnum().WithMessage("Invalid priority value.");
        }
    }
}

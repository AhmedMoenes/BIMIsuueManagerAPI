namespace Application.Validators.Projects
{
    public class CreateProjectDtoValidator : AbstractValidator<CreateProjectDto>
    {
        public CreateProjectDtoValidator()
        {
            RuleFor(x => x.ProjectName)
                .NotEmpty().WithMessage("Project name is required.")
                .MinimumLength(3).WithMessage("Project name must be at least 3 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Description));

            RuleFor(x => x.CompanyId)
                .GreaterThan(0).WithMessage("Company ID must be valid.");

            RuleFor(x => x.StartDate)
                .NotNull().WithMessage("Start date is required.");

            RuleFor(x => x.EndDate)
                .NotNull().WithMessage("End date is required.")
                .GreaterThan(x => x.StartDate)
                .WithMessage("End date must be after the start date.");
        }
    }
}

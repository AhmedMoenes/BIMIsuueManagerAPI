namespace Application.Validators.CompanyProject
{
    public class AssignCompaniesToProjectDtoValidator : AbstractValidator<AssignCompaniesToProjectDto>
    {
        public AssignCompaniesToProjectDtoValidator()
        {
            RuleFor(x => x.ProjectId)
                .GreaterThan(0).WithMessage("ProjectId must be greater than 0");

            RuleFor(x => x.CompanyIds)
                .NotEmpty().WithMessage("At least one company must be assigned.");
        }
    }
}

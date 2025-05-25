namespace Application.Validators
{
    public class UpdateProjectDtoValidator : AbstractValidator<UpdateProjectDto>
    {
        public UpdateProjectDtoValidator()
        {
            RuleFor(x => x.ProjectName)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.CompanyId)
                .GreaterThan(0);
        }
    }
}

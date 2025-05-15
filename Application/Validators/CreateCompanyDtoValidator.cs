namespace Application.Validators
{
    public class CreateCompanyDtoValidator : AbstractValidator<CreateCompanyDto>
    {
        public CreateCompanyDtoValidator()
        {
            RuleFor(x => x.CompanyName)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.SubscriberId)
                .GreaterThan(0);
        }
    }
}

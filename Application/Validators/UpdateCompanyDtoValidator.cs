using Application.DTOs.Company;

namespace Application.Validators
{
    public class UpdateCompanyDtoValidator : AbstractValidator<UpdateCompanyDto>
    {
        public UpdateCompanyDtoValidator()
        {
            RuleFor(x => x.CompanyName)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.SubscriberId)
                .GreaterThan(0);
        }
    }
}

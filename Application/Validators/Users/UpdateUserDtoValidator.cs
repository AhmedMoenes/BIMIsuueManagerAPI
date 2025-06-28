namespace Application.Validators.Users
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty();

            RuleFor(x => x.LastName)
                .NotEmpty();

            RuleFor(x => x.CompanyId)
                .GreaterThan(0);

            RuleFor(x => x.PhoneNumber)
                .MaximumLength(50);

            RuleFor(x => x.Position)
                .MaximumLength(100);
        }
    }
}

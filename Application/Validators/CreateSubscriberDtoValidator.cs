namespace Application.Validators
{
    public class CreateSubscriberDtoValidator : AbstractValidator<CreateSubscriberDto>
    {
        public CreateSubscriberDtoValidator()
        {
            RuleFor(x => x.SubscriberName)
                .NotEmpty()
                .MaximumLength(200);
        }
    }
}

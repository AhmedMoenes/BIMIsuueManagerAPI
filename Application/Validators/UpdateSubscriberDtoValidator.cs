﻿using Application.DTOs.Users;

namespace Application.Validators
{
    public class UpdateSubscriberDtoValidator : AbstractValidator<UpdateSubscriberDto>
    {
        public UpdateSubscriberDtoValidator()
        {
            RuleFor(x => x.SubscriberName)
                .NotEmpty()
                .MaximumLength(200);
        }
    }
}

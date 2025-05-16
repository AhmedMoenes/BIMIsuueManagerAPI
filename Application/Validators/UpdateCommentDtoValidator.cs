using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class UpdateCommentDtoValidator : AbstractValidator<Comment>
    {
        public UpdateCommentDtoValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Comment can not be left empty.");

        }
    }
}

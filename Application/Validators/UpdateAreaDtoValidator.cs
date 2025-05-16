using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class UpdateAreaDtoValidator : AbstractValidator<Area>
    {
        public UpdateAreaDtoValidator()
        {
            RuleFor(x => x.AreaName)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}

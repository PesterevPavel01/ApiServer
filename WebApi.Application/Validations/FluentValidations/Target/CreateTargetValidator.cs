using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Dto.Target;

namespace WebApi.Application.Validations.FluentValidations.Target
{
    internal class CreateTargetValidator:AbstractValidator<TargetDto>
    {
        public CreateTargetValidator()
        {
            RuleFor(x => x.Value).NotEmpty();
        }
    }
}

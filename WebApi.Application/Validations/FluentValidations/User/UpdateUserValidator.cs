using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Dto.Document;
using WebApi.Domain.Dto.User;

namespace WebApi.Application.Validations.FluentValidations.User
{
    internal class UpdateUserValidator : AbstractValidator<UserDto>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(255);
        }
    }
}

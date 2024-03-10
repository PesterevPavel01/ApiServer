using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Dto.Document;

namespace WebApi.Application.Validations.FluentValidations.Organization
{
    internal class UpdateOrganizationValidator : AbstractValidator<OrganizationDto>
    {
        public UpdateOrganizationValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(255);
        }
    }
}

using FluentValidation;
using WebApi.Domain.Dto.Document;

namespace WebApi.Application.Validations.FluentValidations.Organization
{
    public class CreateOrganizationValidator : AbstractValidator<OrganizationDto>
    {
        public CreateOrganizationValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(255);
        }
    }
}

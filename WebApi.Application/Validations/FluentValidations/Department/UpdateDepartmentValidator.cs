using FluentValidation;
using WebApi.Domain.Dto.Document;

namespace WebApi.Application.Validations.FluentValidations.Department
{
    internal class UpdateDepartmentValidator : AbstractValidator<DepartmentDto>
    {
        public UpdateDepartmentValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(255);
        }
    }
}

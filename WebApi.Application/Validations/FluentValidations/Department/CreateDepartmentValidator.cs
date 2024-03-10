using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Dto.Document;

namespace WebApi.Application.Validations.FluentValidations.Department
{
    public class CreateDepartmentValidator : AbstractValidator<DepartmentDto>
    {
        public CreateDepartmentValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(255);
        }
    }
}

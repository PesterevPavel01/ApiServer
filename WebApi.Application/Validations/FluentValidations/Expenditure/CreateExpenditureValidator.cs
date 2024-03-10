using FluentValidation;
using WebApi.Domain.Dto.Document;

namespace WebApi.Application.Validations.FluentValidations.Expenditure
{
    public class CreateExpenditureValidator : AbstractValidator<ExpenditureDto>
    {
        public CreateExpenditureValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(255);
        }
    }
}

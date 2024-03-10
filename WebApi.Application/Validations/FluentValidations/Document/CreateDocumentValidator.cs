using FluentValidation;
using System.Data;
using WebApi.Domain.Dto.Document;

namespace WebApi.Application.Validations.FluentValidations.Document
{
    public class CreateDocumentValidator : AbstractValidator<DocumentDto>

    {
        public CreateDocumentValidator()
        {
            RuleFor(x => x.Value).NotEmpty();
            RuleFor(x => x.Expenditure).NotEmpty();
        }
    }
}

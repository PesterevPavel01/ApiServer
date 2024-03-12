using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Dto.Document;

namespace WebApi.Application.Validations.FluentValidations.Document
{
    public class UpdateDocumentValidator : AbstractValidator<DocumentDto>

    {
        public UpdateDocumentValidator()
        {
            RuleFor(x => x.Value).NotEmpty();
            RuleFor(x => x.Date).NotEmpty();
        }
    }
}

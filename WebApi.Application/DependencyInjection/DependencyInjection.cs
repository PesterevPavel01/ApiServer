using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Application.Mapping;
using WebApi.Application.Services;
using WebApi.Application.Validations;
using WebApi.Application.Validations.FluentValidations.Department;
using WebApi.Application.Validations.FluentValidations.Document;
using WebApi.Application.Validations.FluentValidations.Expenditure;
using WebApi.Application.Validations.FluentValidations.Organization;
using WebApi.Domain.Dto.Document;
using WebApi.Domain.Interfaces.Services;
using WebApi.Domain.Interfaces.Validations;

namespace WebApi.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services) 
        {
            //services.AddAutoMapper(typeof(DocumentMapping));
            services.AddAutoMapper(typeof(DepartmentMapping));
            services.AddAutoMapper(typeof(OrganizationMapping));
            services.AddAutoMapper(typeof(ExpenditureMapping));

            InitServices(services);
        }

        private static void InitServices(this IServiceCollection services) 
        {
            services.AddScoped<IDocumentValidator, DocumentValidator>();
            services.AddScoped<IDepartmentValidator, DepartmentValidator>();
            services.AddScoped<IExpenditureValidator, ExpenditureValidator>();
            services.AddScoped<IOrganizationValidator, OrganizationValidator>();

            services.AddScoped<IValidator<DepartmentDto>, CreateDepartmentValidator>();
            services.AddScoped<IValidator<DocumentDto>, CreateDocumentValidator>();
            services.AddScoped<IValidator<ExpenditureDto>, CreateExpenditureValidator>();
            services.AddScoped<IValidator<OrganizationDto>, CreateOrganizationValidator>();

            services.AddScoped<IValidator<DepartmentDto>, UpdateDepartmentValidator>();
            services.AddScoped<IValidator<DocumentDto>, UpdateDocumentValidator>();
            services.AddScoped<IValidator<ExpenditureDto>, UpdateExpenditureValidator>();
            services.AddScoped<IValidator<OrganizationDto>, UpdateOrganizationValidator>();

            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<ICommonService<OrganizationDto, short>, OrganizationService>();
            services.AddScoped<ICommonService<ExpenditureDto, short>, ExpenditureService>();
        }
    }
}

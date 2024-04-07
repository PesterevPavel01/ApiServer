using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Application.Mapping;
using WebApi.Application.Services;
using WebApi.Application.Validations;
using WebApi.Application.Validations.FluentValidations.Department;
using WebApi.Application.Validations.FluentValidations.Document;
using WebApi.Application.Validations.FluentValidations.Expenditure;
using WebApi.Application.Validations.FluentValidations.Organization;
using WebApi.Application.Validations.FluentValidations.Target;
using WebApi.Application.Validations.FluentValidations.User;
using WebApi.Domain.Dto.Document;
using WebApi.Domain.Dto.Target;
using WebApi.Domain.Dto.User;
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
            services.AddAutoMapper(typeof(TargetMapping));

            InitServices(services);
        }

        private static void InitServices(this IServiceCollection services) 
        {
            services.AddScoped<IDocumentValidator, DocumentValidator>();
            services.AddScoped<IDepartmentValidator, DepartmentValidator>();
            services.AddScoped<IExpenditureValidator, ExpenditureValidator>();
            services.AddScoped<IOrganizationValidator, OrganizationValidator>();
            services.AddScoped<IUserValidator, UserValidator>();
            services.AddScoped<ITargetValidator, TargetValidator>();

            services.AddScoped<IValidator<DepartmentDto>, CreateDepartmentValidator>();
            services.AddScoped<IValidator<DocumentDto>, CreateDocumentValidator>();
            services.AddScoped<IValidator<ExpenditureDto>, CreateExpenditureValidator>();
            services.AddScoped<IValidator<OrganizationDto>, CreateOrganizationValidator>();
            services.AddScoped<IValidator<UserDto>, CreateUserValidator>();
            services.AddScoped<IValidator<TargetDto>, CreateTargetValidator>();

            services.AddScoped<IValidator<DepartmentDto>, UpdateDepartmentValidator>();
            services.AddScoped<IValidator<DocumentDto>, UpdateDocumentValidator>();
            services.AddScoped<IValidator<ExpenditureDto>, UpdateExpenditureValidator>();
            services.AddScoped<IValidator<OrganizationDto>, UpdateOrganizationValidator>();
            services.AddScoped<IValidator<UserDto>, UpdateUserValidator>();
            services.AddScoped<IValidator<TargetDto>, UpdateTargetValidator>();

            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<ICommonService<OrganizationDto, short>, OrganizationService>();
            services.AddScoped<ICommonService<ExpenditureDto, short>, ExpenditureService>();
            services.AddScoped<ICommonService <TargetDto , long>, TargetService>();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApi.DAL.Interceptors;
using WebApi.DAL.Repositories;
using WebApi.Domain.Entity;
using WebApi.Domain.Interfaces.Repositories;

namespace WebApi.DAL.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddDataAccessLayer(this IServiceCollection services,IConfiguration configuration) 
        {
            var connectionString = configuration.GetConnectionString("AppDbConnectionString");

            services.AddSingleton<DateInterceptor>();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });

            services.InitRepositories();
        }

        private static void InitRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<Department>, BaseRepository<Department>>();
            services.AddScoped<IBaseRepository<Document>, BaseRepository<Document>>();
            services.AddScoped<IBaseRepository<Expenditure>, BaseRepository<Expenditure>>();
            services.AddScoped<IBaseRepository<Organization>, BaseRepository<Organization>>();
            services.AddScoped<IBaseRepository<User>, BaseRepository<User>>();
            services.AddScoped<IBaseRepository<Target>, BaseRepository<Target>>();
        }
    }
}

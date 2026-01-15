using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Tuya.Domain.Unit;
using Tuya.Infrastructure;

namespace Tuya.Api.DI
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Metodo encargado de realizar las Injection de las clases
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            AddRegisterDBContext(services, configuration);
            AddRegisterApplication(services);
            AddRegisterInfrastructure(services);
            services.AddEndpointsApiExplorer();
            Cors(services);
            return services;
        }

        private static void AddRegisterDBContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(cfg => cfg.UseSqlServer(configuration.GetConnectionString("cnxTuya")));
            services.AddScoped<IUnitOfWork>(x => x.GetRequiredService<AppDbContext>());
        }

        private static void AddRegisterApplication(IServiceCollection services)
        {
            //services.AddAutoMapper(cfg => cfg.AddProfile<EmployeeMapperProfile>(), AppDomain.CurrentDomain.GetAssemblies());
            //services.AddTransient<IEmployeePort, EmployeeUseCase>();
            //services.AddTransient<IUserPort, UserUseCase>();
            //services.AddScoped<IEmployeeProjectPort, EmployeeProjectUseCase>();
        }

        private static void AddRegisterInfrastructure(this IServiceCollection services)
        {
            //services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            //services.AddScoped<IPositionHistoryRepository, PositionHistoryRepository>();
            //services.AddScoped<IUserRepository, UserRepository>();
            //services.AddScoped<IRolRepository, RolRepository>();
            //services.AddScoped<IPositionRepository, PositionRepository>();
            //services.AddScoped<IProjectRepository, ProjectRepository>();
            //services.AddScoped<IEmployeeProjectRepository, EmployeeProjectRepository>();
        }

        private static void Cors(this IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy(name: "politica", builder =>
            {
                builder
                       .AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
        }
    }
}

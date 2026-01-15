using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Tuya.Application.Mappings;
using Tuya.Application.Port;
using Tuya.Application.UseCase;
using Tuya.Domain.IRepository;
using Tuya.Domain.Unit;
using Tuya.Infrastructure;
using Tuya.Infrastructure.Repository;

namespace Tuya.Api.DI
{
    /// <summary>
    /// Clase encargada de realizar las IoC del proyecto
    /// </summary>
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
            AddSwaggerConf(services);
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
            services.AddAutoMapper(cfg => cfg.AddProfile<MapperProfile>(), AppDomain.CurrentDomain.GetAssemblies());
            services.AddTransient<ICustomerService, CustomerUseCase>();
            services.AddTransient<IOrderService, OrderUseCase>();
            services.AddTransient<IProductService, ProductUseCase>();
        }

        private static void AddRegisterInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
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

        private static void AddSwaggerConf(this IServiceCollection services)
        {
            // Agregamos servicios para Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Api Tuya", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }
    }
}

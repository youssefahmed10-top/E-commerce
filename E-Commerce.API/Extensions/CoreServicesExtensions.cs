using Services.Implementations;
using Services;
using ServicesAbstraction.Contracts;
using Shared.Common;

namespace E_Commerce.API.Extensions
{
    public static class CoreServicesExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services,IConfiguration _configuration) 
        {

            services.AddAutoMapper(str => { }, typeof(AssmeblyReferences).Assembly);
            services.AddScoped<IServiceManager, ServiceManager>();

            services.Configure<JwtOptions>(_configuration.GetSection("JwtOptions")); //IOptions

            return services;

        }
    }
}

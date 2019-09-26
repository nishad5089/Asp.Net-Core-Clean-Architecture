
using Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence;

namespace IOC
{
    public static class DependencyContainer
    {
        public static IServiceCollection Dependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructure(configuration);
            services.AddPersistence(configuration);
            return services;
        }
    }
}
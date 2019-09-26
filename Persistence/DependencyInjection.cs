using Application.Interfaces;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence {
    public static class DependencyInjection {
        public static IServiceCollection AddPersistence (this IServiceCollection services, IConfiguration configuration) {
            services.AddDbContext<ILibraryDbContext, LibraryDbContext> (options =>
                options.UseSqlServer (configuration.GetConnectionString ("LibraryDBConnection")));

            return services;
        }
    }
}
using System.Text;
using Application.Interfaces;
using Infrastructure.Auth.JWT;
using Infrastructure.Identity;
using Infrastructure.Identity.Entity;
using Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure {
    public static class DependecyInjection {
        public static IServiceCollection AddInfrastructure (this IServiceCollection services, IConfiguration configuration) {
            services.AddDbContext<ApplicationDbContext> (options =>
                options.UseSqlServer (configuration.GetConnectionString ("LibraryIdentityDBConnection")));
            services.AddDefaultIdentity<ApplicationUser> ()
                .AddRoles<IdentityRole> ()
                .AddEntityFrameworkStores<ApplicationDbContext> ();
            services.AddScoped<IUserManager, UserManagerService> ();

            // services.Configure<JwtSettings> (configuration.GetSection ("JwtSettings"));
            // services.Configure<JwtSettings>(configuration.GetSection("ConfigurationManager"));

            services.AddTransient<IJwtFactory, JwtFactory> ();

            var jwtSettings = new JwtSettings ();
            configuration.Bind (nameof (jwtSettings), jwtSettings);
            services.AddSingleton (jwtSettings);
            // jwt

            var tokenValidationParameters = new TokenValidationParameters {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey (Encoding.ASCII.GetBytes (jwtSettings.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true
            };
            services.AddSingleton (tokenValidationParameters);
            services.AddAuthentication (x => {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer (x => {
                    x.SaveToken = true;
                    x.TokenValidationParameters = tokenValidationParameters;
                });
            services.AddAuthorization ();
            return services;
        }
    }
}
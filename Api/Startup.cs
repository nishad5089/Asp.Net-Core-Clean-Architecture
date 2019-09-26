using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Api.Options;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence;
using Swashbuckle.AspNetCore.Filters;

namespace Api {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddInfrastructure (Configuration);
            services.AddPersistence (Configuration);

            // var jwtSettings = new JwtSettings ();
            // Configuration.Bind (nameof (jwtSettings), jwtSettings);
            // services.AddSingleton (jwtSettings);
            // jwt
            // var tokenValidationParameters = new TokenValidationParameters {
            //     ValidateIssuerSigningKey = true,
            //     IssuerSigningKey = new SymmetricSecurityKey (Encoding.ASCII.GetBytes (jwtSettings.Secret)),
            //     ValidateIssuer = false,
            //     ValidateAudience = false,
            //     RequireExpirationTime = false,
            //     ValidateLifetime = true
            // };
            // services.AddSingleton (tokenValidationParameters);
            // services.AddAuthentication (x => {
            //         x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //         x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //         x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //     })
            //     .AddJwtBearer (x => {
            //         x.SaveToken = true;
            //         x.TokenValidationParameters = tokenValidationParameters;
            //     });
            services.AddAuthorization ();

            services.AddMvc ().SetCompatibilityVersion (CompatibilityVersion.Version_2_2);

            // swagger
            services.AddSwaggerGen (c => {
                c.SwaggerDoc ("v1", new OpenApiInfo {
                    Version = "v1",
                        Title = "ToDo API",
                        Description = "A simple example ASP.NET Core Web API",
                        TermsOfService = new Uri ("https://www.facebook.com/"),
                        Contact = new OpenApiContact {
                            Name = "Shayne Boyer",
                                Url = new Uri ("https://twitter.com/spboyer"),
                                Email = string.Empty,

                        },
                        License = new OpenApiLicense {
                            Name = "Use under LICX",
                                Url = new Uri ("https://example.com/license"),
                        }
                });
                c.ExampleFilters ();
                c.AddSecurityDefinition ("Bearer", new OpenApiSecurityScheme {
                    Description = "JWT Authorization header using the bearer scheme",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement (new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Id = "Bearer",
                                    Type = ReferenceType.SecurityScheme
                            }
                        }, new List<string> ()
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine (AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments (xmlPath);

            });
            services.AddSwaggerExamplesFromAssemblyOf<Startup> ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts ();
            }
            app.UseHttpsRedirection ();
            app.UseDefaultFiles ();

            app.UseAuthentication ();

            var swaggerOptions = new SwaggerOptions ();
            Configuration.GetSection (nameof (SwaggerOptions)).Bind (swaggerOptions);

            app.UseSwagger (option => { option.RouteTemplate = swaggerOptions.JsonRoute; });

            app.UseSwaggerUI (option => {
                option.SwaggerEndpoint (swaggerOptions.UiEndpoint, swaggerOptions.Description);
                option.InjectStylesheet ("/swagger/custom.css");
            });
            app.UseMvc ();
        }
    }
}
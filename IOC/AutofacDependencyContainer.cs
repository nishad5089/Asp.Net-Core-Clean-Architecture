using System;
using Application;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace IOC
{
    public class AutofacDependencyContainer
    {
        public static IServiceProvider RegisterServices(IServiceCollection services)
        {
            // Now register our services with Autofac container.
            var builder = new ContainerBuilder();
            builder.RegisterModule(new ApplicationModule());
            builder.RegisterModule(new InfrastructureModule());
            builder.RegisterModule(new InfrastructureModule());
            builder.Populate(services);
            var container = builder.Build();
            // Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(container);

        }
    }
}
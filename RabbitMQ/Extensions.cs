using System.Reflection;
using MassTransit;
using MassTransit.Definition;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Play.Common.Settings;

namespace Play.Common.RabbitMQ
{
    public static class Extensions
    {
        public static IServiceCollection AddMassTransitWithRabbitMQ(this IServiceCollection services){
            services.AddMassTransit(x => {
               x.AddConsumers(Assembly.GetEntryAssembly());

               x.UsingRabbitMq((context, configurator) => {
                    var configuration = context.GetService<IConfiguration>();

                    var serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
                    var rabbitMQSettings = configuration.GetSection(nameof(RabbitMQSettings)).Get<RabbitMQSettings>();

                    configurator.Host(rabbitMQSettings.Host);
                    configurator.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(serviceSettings.Name, false));
               });
            });

            services.AddMassTransitHostedService();

            return services;
        }
    }
}
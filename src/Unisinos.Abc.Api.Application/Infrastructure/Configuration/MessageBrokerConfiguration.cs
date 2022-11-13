using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Unisinos.Abc.Api.Application.Infrastructure.Integrations;
using Unisinos.Abc.Api.Application.Interfaces.Integrations;
using Unisinos.Abc.Messages;
using Unisinos.Abc.Messages.Commands;

namespace Unisinos.Abc.Api.Application.Infrastructure.Configuration
{
    public static class MessageBrokerConfiguration
    {
        public static void AddMessageBrokerConfirurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IMessageBrokerService, MessageBrokerService>();

            services.AddMassTransit(x =>
            {
                var kafkaHost = configuration.GetSection("ConnectionStrings:KafkaBrokerHost").Value;

                x.UsingInMemory((context, cfg) =>
                {
                    cfg.ConfigureEndpoints(context);
                });

                x.AddRider(rider =>
                {

                    rider.AddProducer<PurchaseCreditCourseCommand>(Topics.PurchaseCreditCourse.PurchaseCreditCourseCommand);

                    rider.SetKebabCaseEndpointNameFormatter();

                    rider.UsingKafka((context, k) =>
                    {
                        k.Host(kafkaHost);
                    });
                });
            });

            services.AddTransient<IMessageBrokerService, MessageBrokerService>();
        }
    }
}

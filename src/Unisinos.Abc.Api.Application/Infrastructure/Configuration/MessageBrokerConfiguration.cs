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
                var eventHubsUserName = configuration.GetSection("ConnectionStrings:AzureEventHubsUserName").Value;
                var eventHubsConnection = configuration.GetSection("ConnectionStrings:AzureEventHubs").Value;
                var eventHubsHost = configuration.GetSection("ConnectionStrings:AzureEventHubsHost").Value;

                x.UsingAzureServiceBus((context, cfg) =>
                {
                    cfg.Host(eventHubsConnection);
                    cfg.ConfigureEndpoints(context);
                });

                x.AddRider(rider =>
                {

                    rider.AddProducer<PurchaseCreditCourseCommand>(Topics.PurchaseCreditCourse.PurchaseCreditCourseCommand);

                    rider.SetKebabCaseEndpointNameFormatter();

                    rider.UsingKafka(new()
                    {
                        SecurityProtocol = Confluent.Kafka.SecurityProtocol.SaslSsl,
                        SaslMechanism = Confluent.Kafka.SaslMechanism.Plain,
                        SaslUsername = eventHubsUserName,
                        SaslPassword = eventHubsConnection
                    }, (context, k) =>
                    {
                        k.Host(eventHubsHost);
                    });
                });
            });

            services.AddTransient<IMessageBrokerService, MessageBrokerService>();
        }
    }
}

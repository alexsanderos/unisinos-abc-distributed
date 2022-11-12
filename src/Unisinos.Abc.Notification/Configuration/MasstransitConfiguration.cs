using Confluent.Kafka;
using MassTransit;
using Unisinos.Abc.Notification.Consumers;
using Unisinos.Abc.Messages;
using Unisinos.Abc.Messages.Commands;

namespace Unisinos.Abc.Notification.Configuration
{
    public static class MasstransitConfiguration
    {
        public static void AddMasstransitConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
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
                    rider.AddConsumer<NotificationConsumer>();

                    rider.SetKebabCaseEndpointNameFormatter();

                    rider.UsingKafka(new()
                    {
                        SecurityProtocol = SecurityProtocol.SaslSsl,
                        SaslMechanism = SaslMechanism.Plain,
                        SaslUsername = eventHubsUserName,
                        SaslPassword = eventHubsConnection
                    }, (context, k) =>
                    {
                        k.Host(eventHubsHost);

                        k.TopicEndpoint<NotificationCommand>(
                            Topics.Notification.NotificationCommand, Topics.DefaultGroup.DefaultGroupId, e =>
                        {
                            e.Consumer<NotificationConsumer>(context);

                            e.AutoOffsetReset = AutoOffsetReset.Earliest;
                            e.CreateIfMissing(t =>
                            {
                                t.NumPartitions = 1;
                                t.ReplicationFactor = 2;
                            });
                        });
                    });
                });
            });
        }
    }
}

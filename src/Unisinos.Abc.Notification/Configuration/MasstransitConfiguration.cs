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
                var kafkaHost = configuration.GetSection("ConnectionStrings:KafkaBrokerHost").Value;

                x.UsingInMemory((context, cfg) =>
                {
                    cfg.ConfigureEndpoints(context);
                });

                x.AddRider(rider =>
                {
                    rider.AddConsumer<NotificationConsumer>();

                    rider.SetKebabCaseEndpointNameFormatter();

                    rider.UsingKafka((context, k) =>
                    {
                        k.Host(kafkaHost);

                        k.TopicEndpoint<NotificationCommand>(
                            Topics.Notification.NotificationCommand, Topics.Notification.NotificationCommandGroupId, e =>
                        {
                            e.Consumer<NotificationConsumer>(context);

                            e.AutoOffsetReset = AutoOffsetReset.Earliest;
                            e.CreateIfMissing(t =>
                            {
                                t.NumPartitions = 1;
                                t.ReplicationFactor = 1;
                            });
                        });
                    });
                });
            });
        }
    }
}

using Confluent.Kafka;
using MassTransit;
using Unisinos.Abc.AnalyzeProfile.Consumers;
using Unisinos.Abc.Messages;
using Unisinos.Abc.Messages.Commands;
using Unisinos.Abc.Messages.Events;

namespace Unisinos.Abc.AnalyzeProfile.Configuration
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

                    rider.AddProducer<IApprovedProfileEvent>(Topics.AnalyzeProfile.ApprovedProfileEvent);
                    rider.AddProducer<IDisapprovedProfileEvent>(Topics.AnalyzeProfile.DisapprovedProfileEvent);

                    rider.AddConsumer<AnalyzeProfileCreditConsumer>();

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

                        k.TopicEndpoint<AnalyzeProfileCreditCommand>(
                            Topics.AnalyzeProfile.AnalyzeProfileCreditCommand, Topics.DefaultGroup.DefaultGroupId, e =>
                        {
                            e.Consumer<AnalyzeProfileCreditConsumer>(context);

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

using Confluent.Kafka;
using MassTransit;
using Unisinos.Abc.Messages;
using Unisinos.Abc.Messages.Commands;
using Unisinos.Abc.Messages.Events;
using Unisinos.Abc.Payment.Consumers;

namespace Unisinos.Abc.Payment.Configuration
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

                    rider.AddProducer<IPaymentCompletedEvent>(Topics.PaymentProcess.PaymentCompletedEvent);
                    rider.AddProducer<IPaymentErrorEvent>(Topics.PaymentProcess.PaymentErrorEvent);

                    rider.AddConsumer<PaymentProcessConsumer>();

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

                        k.TopicEndpoint<PaymentProcessCommand>(
                            Topics.PaymentProcess.PaymentProcessCommand, Topics.DefaultGroup.DefaultGroupId, e =>
                        {
                            e.Consumer<PaymentProcessConsumer>(context);

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

using Confluent.Kafka;
using MassTransit;
using Unisinos.Abc.Messages;
using Unisinos.Abc.Messages.Commands;
using Unisinos.Abc.Messages.Events;
using Unisinos.Abc.Payment.Consumers;

namespace Unisinos.Abc.Payment.Infrastructure.Configuration
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

                    rider.AddProducer<IPaymentCompletedEvent>(Topics.PaymentProcess.PaymentCompletedEvent);
                    rider.AddProducer<IPaymentErrorEvent>(Topics.PaymentProcess.PaymentErrorEvent);

                    rider.AddConsumer<PaymentProcessConsumer>();

                    rider.SetKebabCaseEndpointNameFormatter();

                    rider.UsingKafka((context, k) =>
                    {
                        k.Host(kafkaHost);

                        k.TopicEndpoint<PaymentProcessCommand>(
                            Topics.PaymentProcess.PaymentProcessCommand, Topics.DefaultGroup.DefaultGroupId, e =>
                        {
                            e.Consumer<PaymentProcessConsumer>(context);

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

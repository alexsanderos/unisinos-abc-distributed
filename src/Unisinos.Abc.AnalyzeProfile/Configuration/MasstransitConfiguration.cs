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
                var kafkaHost = configuration.GetSection("ConnectionStrings:KafkaBrokerHost").Value;

                x.UsingInMemory((context, cfg) =>
                {
                    cfg.ConfigureEndpoints(context);
                });

                x.AddRider(rider =>
                {

                    rider.AddProducer<IApprovedProfileEvent>(Topics.AnalyzeProfile.ApprovedProfileEvent);
                    rider.AddProducer<IDisapprovedProfileEvent>(Topics.AnalyzeProfile.DisapprovedProfileEvent);

                    rider.AddConsumer<AnalyzeProfileCreditConsumer>();

                    rider.SetKebabCaseEndpointNameFormatter();

                    rider.UsingKafka((context, k) =>
                    {
                        k.Host(kafkaHost);

                        k.TopicEndpoint<AnalyzeProfileCreditCommand>(
                            Topics.AnalyzeProfile.AnalyzeProfileCreditCommand, Topics.DefaultGroup.DefaultGroupId, e =>
                        {
                            e.Consumer<AnalyzeProfileCreditConsumer>(context);

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

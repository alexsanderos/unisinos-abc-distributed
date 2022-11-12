using Confluent.Kafka;
using MassTransit;
using Unisinos.Abc.Messages;
using Unisinos.Abc.Messages.Commands;
using Unisinos.Abc.Messages.Events;
using Unisinos.Abc.Orchestrator.Sagas;

namespace Unisinos.Abc.Orchestrator.Infrastructure.Configuration
{
    public static class MasstransitConfiguration
    {
        public static void AddMasstransitConfirurations(
            this IServiceCollection services, IConfiguration configuration)
        {
            var eventHubsUserName = configuration.GetSection("ConnectionStrings:AzureEventHubsUserName").Value;
            var eventHubsConnection = configuration.GetSection("ConnectionStrings:AzureEventHubs").Value;
            var eventHubsHost = configuration.GetSection("ConnectionStrings:AzureEventHubsHost").Value;
            var eventHubsKey = configuration.GetSection("ConnectionStrings:AzureEventHubsKey").Value;

            var mongoUri = configuration.GetSection("ConnectionStrings:DatabaseUri").Value;
            var mongoKey = configuration.GetSection("ConnectionStrings:DatabaseKey").Value;
            var mongoDatabaseName = configuration.GetSection("ConnectionStrings:DatabaseName").Value;
            var mongoDatabaseConnection = configuration.GetSection("ConnectionStrings:DatabaseConnection").Value;

            services.AddMassTransit(x =>
            {
                x.UsingInMemory((context, cfg) =>
                {
                    cfg.ConfigureEndpoints(context);
                });

                x.AddRider(rider =>
                {
                    rider.AddSagaStateMachine<PurchaseCourseStateMachine, PurchaseCourseState>()
                        .MongoDbRepository(r =>
                        {
                            r.Connection = mongoDatabaseConnection;
                            r.DatabaseName = mongoDatabaseName;
                            r.CollectionName = "sagas";
                        });

                    rider.AddProducer<AnalyzeProfileCreditCommand>(Topics.AnalyzeProfile.AnalyzeProfileCreditCommand);
                    rider.AddProducer<PaymentProcessCommand>(Topics.PaymentProcess.PaymentProcessCommand);
                    rider.AddProducer<NotificationCommand>(Topics.Notification.NotificationCommand);

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

                        k.TopicEndpoint<PurchaseCreditCourseCommand>(
                            Topics.PurchaseCreditCourse.PurchaseCreditCourseCommand, Topics.DefaultGroup.DefaultGroupId, e =>
                        {
                            e.ConfigureSaga<PurchaseCourseState>(context);
                            e.AutoOffsetReset = AutoOffsetReset.Earliest;

                            e.CreateIfMissing(t =>
                            {
                                t.NumPartitions = 1;
                                t.ReplicationFactor = 2;
                            });
                        });

                        k.TopicEndpoint<IApprovedProfileEvent>(
                            Topics.AnalyzeProfile.ApprovedProfileEvent, Topics.DefaultGroup.DefaultGroupId, e =>
                        {
                            e.ConfigureSaga<PurchaseCourseState>(context);
                            e.AutoOffsetReset = AutoOffsetReset.Earliest;
                            e.CreateIfMissing(t =>
                            {
                                t.NumPartitions = 1;
                                t.ReplicationFactor = 2;
                            });
                        });

                        k.TopicEndpoint<IDisapprovedProfileEvent>(
                            Topics.AnalyzeProfile.DisapprovedProfileEvent, Topics.DefaultGroup.DefaultGroupId, e =>
                        {
                            e.ConfigureSaga<PurchaseCourseState>(context);
                            e.AutoOffsetReset = AutoOffsetReset.Earliest;
                            e.CreateIfMissing(t =>
                            {
                                t.NumPartitions = 1;
                                t.ReplicationFactor = 2;
                            });
                        });

                        k.TopicEndpoint<IPaymentCompletedEvent>(
                            Topics.PaymentProcess.PaymentCompletedEvent, Topics.DefaultGroup.DefaultGroupId, e =>
                        {
                            e.ConfigureSaga<PurchaseCourseState>(context);
                            e.AutoOffsetReset = AutoOffsetReset.Earliest;
                            e.CreateIfMissing(t =>
                            {
                                t.NumPartitions = 1;
                                t.ReplicationFactor = 2;
                            });
                        });

                        k.TopicEndpoint<IPaymentErrorEvent>(
                            Topics.PaymentProcess.PaymentErrorEvent, Topics.DefaultGroup.DefaultGroupId, e =>
                        {
                            e.ConfigureSaga<PurchaseCourseState>(context);
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

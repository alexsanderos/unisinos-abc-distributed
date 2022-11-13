using Confluent.Kafka;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Unisinos.Abc.Messages;
using Unisinos.Abc.Messages.Commands;
using Unisinos.Abc.Messages.Events;
using Unisinos.Abc.Orchestrator.Infrastructure.Data.Context;
using Unisinos.Abc.Orchestrator.Sagas;

namespace Unisinos.Abc.Orchestrator.Infrastructure.Configuration
{
    public static class MasstransitConfiguration
    {
        public static void AddMasstransitConfirurations(
            this IServiceCollection services, IConfiguration configuration)
        {
            var sqlConnection = configuration.GetSection("ConnectionStrings:DatabaseConnection").Value;
            var kafkaHost = configuration.GetSection("ConnectionStrings:KafkaBrokerHost").Value;

            services.AddMassTransit(x =>
            {
                x.UsingInMemory((context, cfg) =>
                {
                    cfg.ConfigureEndpoints(context);
                });

                x.AddRider(rider =>
                {
                    rider.AddSagaStateMachine<PurchaseCourseStateMachine, PurchaseCourseState>()
                        .EntityFrameworkRepository(repository =>
                        {
                            repository.ConcurrencyMode = ConcurrencyMode.Pessimistic;

                            repository.AddDbContext<DbContext, UnisinosAbcContext>((provider, builder) =>
                            {
                                builder.UseSqlServer(sqlConnection);
                            });
                        });

                    rider.AddProducer<AnalyzeProfileCreditCommand>(Topics.AnalyzeProfile.AnalyzeProfileCreditCommand);
                    rider.AddProducer<PaymentProcessCommand>(Topics.PaymentProcess.PaymentProcessCommand);
                    rider.AddProducer<NotificationCommand>(Topics.Notification.NotificationCommand);

                    rider.SetKebabCaseEndpointNameFormatter();

                    rider.UsingKafka((context, k) =>
                    {
                        k.Host(kafkaHost);

                        k.TopicEndpoint<PurchaseCreditCourseCommand>(
                            Topics.PurchaseCreditCourse.PurchaseCreditCourseCommand, Topics.PurchaseCreditCourse.PurchaseCreditCourseCommandGroupId, e =>
                        {
                            e.ConfigureSaga<PurchaseCourseState>(context);
                            e.AutoOffsetReset = AutoOffsetReset.Earliest;

                            e.CreateIfMissing(t =>
                            {
                                t.NumPartitions = 1;
                                t.ReplicationFactor = 1;
                            });
                        });

                        k.TopicEndpoint<IApprovedProfileEvent>(
                            Topics.AnalyzeProfile.ApprovedProfileEvent, Topics.AnalyzeProfile.ApprovedProfileEventGroupId, e =>
                        {
                            e.ConfigureSaga<PurchaseCourseState>(context);
                            e.AutoOffsetReset = AutoOffsetReset.Earliest;
                            e.CreateIfMissing(t =>
                            {
                                t.NumPartitions = 1;
                                t.ReplicationFactor = 1;
                            });
                        });

                        k.TopicEndpoint<IDisapprovedProfileEvent>(
                            Topics.AnalyzeProfile.DisapprovedProfileEvent, Topics.AnalyzeProfile.DisapprovedProfileEventGroupId, e =>
                        {
                            e.ConfigureSaga<PurchaseCourseState>(context);
                            e.AutoOffsetReset = AutoOffsetReset.Earliest;
                            e.CreateIfMissing(t =>
                            {
                                t.NumPartitions = 1;
                                t.ReplicationFactor = 1;
                            });
                        });

                        k.TopicEndpoint<IPaymentCompletedEvent>(
                            Topics.PaymentProcess.PaymentCompletedEvent, Topics.PaymentProcess.PaymentCompletedEventGroupId, e =>
                        {
                            e.ConfigureSaga<PurchaseCourseState>(context);
                            e.AutoOffsetReset = AutoOffsetReset.Earliest;
                            e.CreateIfMissing(t =>
                            {
                                t.NumPartitions = 1;
                                t.ReplicationFactor = 1;
                            });
                        });

                        k.TopicEndpoint<IPaymentErrorEvent>(
                            Topics.PaymentProcess.PaymentErrorEvent, Topics.PaymentProcess.PaymentErrorEventGroupId, e =>
                        {
                            e.ConfigureSaga<PurchaseCourseState>(context);
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

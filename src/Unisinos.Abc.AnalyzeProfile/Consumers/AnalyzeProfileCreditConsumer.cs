using MassTransit;
using Unisinos.Abc.Messages.Commands;
using Unisinos.Abc.Messages.Events;

namespace Unisinos.Abc.AnalyzeProfile.Consumers
{
    public class AnalyzeProfileCreditConsumer : IConsumer<AnalyzeProfileCreditCommand>
    {
        private readonly ITopicProducer<IApprovedProfileEvent> _topicApproved;
        private readonly ITopicProducer<IDisapprovedProfileEvent> _topicDisapproved;
        private readonly ILogger<AnalyzeProfileCreditConsumer> _logger;

        public AnalyzeProfileCreditConsumer(
            ITopicProducer<IApprovedProfileEvent> topicApproved,
            ITopicProducer<IDisapprovedProfileEvent> topicDisapproved,
            ILogger<AnalyzeProfileCreditConsumer> logger)
        {
            _topicApproved = topicApproved;
            _topicDisapproved = topicDisapproved;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<AnalyzeProfileCreditCommand> context)
        {
            //TODO: process validation approved
            _logger.LogInformation("Consume information message analyze profile credit correlationId:{0}", context.Message.CorrelationId);

            await _topicApproved.Produce(
                new
                {
                    CorrelationId = context.Message.CorrelationId,
                    StudentName = context.Message.StudentName,
                    StudentCPF = context.Message.StudentCPF,
                    TotalValue = context.Message.TotalValue,
                    NumberInstallments = context.Message.NumberInstallments
                });
        }
    }
}

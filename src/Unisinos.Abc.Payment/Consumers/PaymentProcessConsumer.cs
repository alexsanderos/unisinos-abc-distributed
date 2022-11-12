using MassTransit;
using Unisinos.Abc.Messages.Commands;
using Unisinos.Abc.Messages.Events;

namespace Unisinos.Abc.Payment.Consumers
{
    public class PaymentProcessConsumer : IConsumer<PaymentProcessCommand>
    {
        private readonly ITopicProducer<IPaymentCompletedEvent> _topicProducerCompleted;
        private readonly ITopicProducer<IPaymentErrorEvent> _topicProducerError;
        private readonly ILogger<PaymentProcessConsumer> _logger;
        public PaymentProcessConsumer(
            ITopicProducer<IPaymentCompletedEvent> topicProducerCompleted,
            ITopicProducer<IPaymentErrorEvent> topicProducerError,
            ILogger<PaymentProcessConsumer> logger)
        {
            _topicProducerCompleted = topicProducerCompleted;
            _topicProducerError = topicProducerError;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<PaymentProcessCommand> context)
        {
            //TODO: process payment
            _logger.LogInformation("Consume information message payment process correlationId:{0}", context.Message.CorrelationId);


            await _topicProducerCompleted.Produce(
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

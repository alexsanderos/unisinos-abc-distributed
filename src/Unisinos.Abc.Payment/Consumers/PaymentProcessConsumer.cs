using MassTransit;
using Unisinos.Abc.Messages.Commands;
using Unisinos.Abc.Messages.Events;
using Unisinos.Abc.Payment.Entities;
using Unisinos.Abc.Payment.Interfaces.Repositories;

namespace Unisinos.Abc.Payment.Consumers
{
    public class PaymentProcessConsumer : IConsumer<PaymentProcessCommand>
    {
        private readonly ITopicProducer<IPaymentCompletedEvent> _topicProducerCompleted;
        private readonly ITopicProducer<IPaymentErrorEvent> _topicProducerError;
        private readonly IPaymentRepository _paymentRepository;
        private readonly ILogger<PaymentProcessConsumer> _logger;
        public PaymentProcessConsumer(
            ITopicProducer<IPaymentCompletedEvent> topicProducerCompleted,
            ITopicProducer<IPaymentErrorEvent> topicProducerError,
            IPaymentRepository paymentRepository,
            ILogger<PaymentProcessConsumer> logger)
        {
            _topicProducerCompleted = topicProducerCompleted;
            _topicProducerError = topicProducerError;
            _paymentRepository = paymentRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<PaymentProcessCommand> context)
        {
            //TODO: process payment
            _logger.LogInformation("Consume information message payment process correlationId:{0}", context.Message.CorrelationId);

            var completedEntity = new
            {
                CorrelationId = context.Message.CorrelationId,
                StudentName = context.Message.StudentName,
                StudentCPF = context.Message.StudentCPF,
                TotalValue = context.Message.TotalValue,
                NumberInstallments = context.Message.NumberInstallments
            };

            _paymentRepository.Add(new PaymentEntity()
            {
                Id = Guid.NewGuid(),
                CorrelationId = completedEntity.CorrelationId,
                CPF = completedEntity.StudentCPF,
                Name = completedEntity.StudentName,
                NumberInstallments = completedEntity.NumberInstallments,
                TotalValue = completedEntity.TotalValue
            });


            await _topicProducerCompleted.Produce(completedEntity);
        }
    }
}

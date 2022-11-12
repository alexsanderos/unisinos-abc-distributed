using Unisinos.Abc.Api.Application.Interfaces.Integrations;
using MassTransit;
using Unisinos.Abc.Messages.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Unisinos.Abc.Api.Application.Infrastructure.Integrations
{
    public class MessageBrokerService : IMessageBrokerService
    {
        private readonly ITopicProducer<PurchaseCreditCourseCommand> _topicProducer;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<MessageBrokerService> _logger;

        public MessageBrokerService(
            IServiceScopeFactory serviceScopeFactory,
            ILogger<MessageBrokerService> logger)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            var scope = _serviceScopeFactory.CreateScope();
            _topicProducer = scope.ServiceProvider.GetService<ITopicProducer<PurchaseCreditCourseCommand>>();
        }

        public async Task SendMessage(PurchaseCreditCourseCommand message)
        {
            try
            {
                await _topicProducer.Produce(
                    new {
                        CorrelationId = message.CorrelationId,
                        StudentName = message.StudentName,
                        StudentCPF = message.StudentCPF,
                        TotalValue = message.TotalValue,
                        NumberInstallments = message.NumberInstallments
                    });
            } catch(Exception e)
            {
                _logger.LogError(e, "Error produce message");
            }
        }
    }
}

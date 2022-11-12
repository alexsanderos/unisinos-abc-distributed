using MassTransit;
using Unisinos.Abc.Messages.Commands;

namespace Unisinos.Abc.Notification.Consumers
{
    public class NotificationConsumer : IConsumer<NotificationCommand>
    {
        private readonly ILogger<NotificationConsumer> _logger;
        public NotificationConsumer(ILogger<NotificationConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<NotificationCommand> context)
        {
            //TODO: send notification approved
            _logger.LogInformation("Consume information message notification correlationId:{0}", context.Message.CorrelationId);
        }
    }
}

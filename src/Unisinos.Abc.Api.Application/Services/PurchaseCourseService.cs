using Microsoft.Extensions.Logging;
using Unisinos.Abc.Api.Application.Interfaces.Integrations;
using Unisinos.Abc.Api.Application.Interfaces.Services;
using Unisinos.Abc.Messages.Commands;

namespace Unisinos.Abc.Api.Application.Services
{
    public class PurchaseCourseService : IPurchaseCourseService
    {
        private readonly IMessageBrokerService _messageBrokerService;
        private readonly ILogger<PurchaseCourseService> _logger;

        public PurchaseCourseService(
            IMessageBrokerService messageBrokerService,
            ILogger<PurchaseCourseService> logger)
        {
            _messageBrokerService = messageBrokerService;
            _logger = logger;
        }

        public async Task PurchaseCreditCourse(PurchaseCreditCourseCommand command)
        {
            try
            {
                _logger.LogInformation("Send purchase credit course message to broker correlationId:{0}", command.CorrelationId);
                await _messageBrokerService.SendMessage(command);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error purchase course");
            }
        }
    }
}

using Unisinos.Abc.Messages.Commands;

namespace Unisinos.Abc.Api.Application.Interfaces.Integrations
{
    public interface IMessageBrokerService
    {
        Task SendMessage(PurchaseCreditCourseCommand message);
    }
}

namespace Unisinos.Abc.Messages.Events
{
    public interface IBaseEvent
    {
        Guid CorrelationId { get; set; }
    }
}

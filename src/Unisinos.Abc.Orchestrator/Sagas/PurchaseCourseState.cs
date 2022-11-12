using MassTransit;
namespace Unisinos.Abc.Orchestrator.Sagas
{
    public class PurchaseCourseState : SagaStateMachineInstance, ISagaVersion
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; }
        public int Version { get; set; }
    }
}

using MassTransit;
using Unisinos.Abc.Messages.Commands;
using Unisinos.Abc.Messages.Events;
using Unisinos.Abc.Orchestrator.Sagas.Producers;

namespace Unisinos.Abc.Orchestrator.Sagas
{
    public class PurchaseCourseStateMachine : MassTransitStateMachine<PurchaseCourseState>
    {
        private readonly ILogger<PurchaseCourseStateMachine> _logger;
        private readonly int _version = 1;

        public PurchaseCourseStateMachine(ILogger<PurchaseCourseStateMachine> logger)
        {
            _logger = logger;

            InstanceState(x => x.CurrentState);

            ConfigureCorrelationIds();

            Initially(
                When(OnPurchaseCourseCredit)
                    .Then(context =>
                    {
                        context.Saga.CorrelationId = context.Message.CorrelationId;
                        context.Saga.Version = _version;
                    })
                    .Then(context => _logger.LogInformation("Started analyze profile for correlationId:{0}", context.Message.CorrelationId))
                    .Activity(x => x.OfInstanceType<AnalyzeProfileCreditActivity>())
              .TransitionTo(AwaitCreditAnalysis));

            During(AwaitCreditAnalysis,
                When(OnApprovedProfile)
                    .Then(context => _logger.LogInformation("Started payment process for correlationId:{0}", context.Message.CorrelationId))
                    .Activity(x => x.OfInstanceType<PaymentProcessActivity>())
                    .TransitionTo(AwaitProcessPayment),
                When(OnDisapprovedProfile)
                    .Then(context => _logger.LogInformation("Disapproved profile for correlationId:{0}", context.Message.CorrelationId))
                    .TransitionTo(CompletedError)
            );

            During(AwaitProcessPayment,
                When(OnPaymentSuccess)
                    .Then(context => _logger.LogInformation("Payment Processed for correlationId:{0}", context.Message.CorrelationId))
                    .Then(context => _logger.LogInformation("Send notification"))
                    .Activity(x => x.OfInstanceType<NotificationActivity>())
                    .TransitionTo(Completed),
                When(OnPaymentError)
                    .Then(context => _logger.LogInformation("Payment error for correlationId:{0}", context.Message.CorrelationId))
                    .TransitionTo(CompletedError)
            );
        }

        public Event<PurchaseCreditCourseCommand> OnPurchaseCourseCredit { get; private set; }
        public Event<IApprovedProfileEvent> OnApprovedProfile { get; private set; }
        public Event<IDisapprovedProfileEvent> OnDisapprovedProfile { get; private set; }
        public Event<IPaymentCompletedEvent> OnPaymentSuccess { get; private set; }
        public Event<IPaymentErrorEvent> OnPaymentError { get; private set; }

        public State AwaitCreditAnalysis { get; private set; }
        public State AwaitProcessPayment { get; private set; }
        public State Completed { get; private set; }
        public State CompletedError { get; private set; }

        private void ConfigureCorrelationIds()
        {
            Event(() => OnPurchaseCourseCredit, x => x.CorrelateById(x => x.Message.CorrelationId));
            Event(() => OnApprovedProfile, x => x.CorrelateById(x => x.Message.CorrelationId));
            Event(() => OnDisapprovedProfile, x => x.CorrelateById(x => x.Message.CorrelationId));
            Event(() => OnPaymentSuccess, x => x.CorrelateById(x => x.Message.CorrelationId));
            Event(() => OnPaymentError, x => x.CorrelateById(x => x.Message.CorrelationId));
        }
    }
}

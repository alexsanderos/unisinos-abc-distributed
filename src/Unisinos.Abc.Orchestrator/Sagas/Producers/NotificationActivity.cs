using MassTransit;
using Unisinos.Abc.Messages.Commands;
using Unisinos.Abc.Messages.Events;

namespace Unisinos.Abc.Orchestrator.Sagas.Producers
{
    public class NotificationActivity : IStateMachineActivity<PurchaseCourseState>
    {
        private readonly ITopicProducer<NotificationCommand> _topicProducer;
        private readonly ILogger<NotificationActivity> _logger;

        public NotificationActivity(
            IServiceScopeFactory serviceScopeFactory,
            ILogger<NotificationActivity> logger)
        {
            var scope = serviceScopeFactory.CreateScope();
            _topicProducer = scope.ServiceProvider.GetService<ITopicProducer<NotificationCommand>>();
            _logger = logger;
        }

        public void Accept(StateMachineVisitor visitor)
        {
            visitor.Visit(this);
        }

        public async Task Execute(BehaviorContext<PurchaseCourseState> context, IBehavior<PurchaseCourseState> next)
        {
            await Execute((BehaviorContext<PurchaseCourseState, IPaymentCompletedEvent>)context);
            await next.Execute(context);
        }

        public async Task Execute<T>(BehaviorContext<PurchaseCourseState, T> context, IBehavior<PurchaseCourseState, T> next) where T : class
        {
            await Execute((BehaviorContext<PurchaseCourseState, IPaymentCompletedEvent>)context);
            await next.Execute(context);
        }

        public Task Faulted<TException>(BehaviorExceptionContext<PurchaseCourseState, TException> context, IBehavior<PurchaseCourseState> next) where TException : Exception
        {
            return next.Faulted(context);
        }

        public Task Faulted<T, TException>(BehaviorExceptionContext<PurchaseCourseState, T, TException> context, IBehavior<PurchaseCourseState, T> next)
            where T : class
            where TException : Exception
        {
            return next.Faulted(context);
        }

        public void Probe(ProbeContext context)
        {
            context.CreateScope(typeof(NotificationCommand).ToString());
        }

        async Task Execute(BehaviorContext<PurchaseCourseState, IPaymentCompletedEvent> context)
        {
            _logger.LogInformation("Send message notification to broker correlationId:{0}", context.Message.CorrelationId);

            await _topicProducer.Produce(new
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

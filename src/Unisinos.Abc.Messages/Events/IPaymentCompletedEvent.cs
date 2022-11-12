namespace Unisinos.Abc.Messages.Events
{
    public interface IPaymentCompletedEvent : IBaseEvent
    {
        string? StudentName { get; set; }
        string? StudentCPF { get; set; }
        public decimal TotalValue { get; set; }
        public int NumberInstallments { get; set; }
    }
}

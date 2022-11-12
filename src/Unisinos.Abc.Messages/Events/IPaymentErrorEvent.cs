namespace Unisinos.Abc.Messages.Events
{
    public interface IPaymentErrorEvent : IBaseEvent
    {
        Guid Id { get; set; }
        string? StudentCPF { get; set; }
        public decimal TotalValue { get; set; }
        public int NumberInstallments { get; set; }
    }
}

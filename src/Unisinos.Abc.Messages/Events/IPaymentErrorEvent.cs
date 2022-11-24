namespace Unisinos.Abc.Messages.Events
{
    public interface IPaymentErrorEvent : IBaseEvent
    {
        string? StudentCPF { get; set; }
        public decimal TotalValue { get; set; }
        public int NumberInstallments { get; set; }
    }
}

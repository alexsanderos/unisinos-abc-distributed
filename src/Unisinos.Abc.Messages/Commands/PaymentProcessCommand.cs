namespace Unisinos.Abc.Messages.Commands
{
    public class PaymentProcessCommand
    {
        public Guid CorrelationId { get; set; }
        public string? StudentName { get; set; }
        public string? StudentCPF { get; set; }
        public decimal TotalValue { get; set; }
        public int NumberInstallments { get; set; }
    }
}

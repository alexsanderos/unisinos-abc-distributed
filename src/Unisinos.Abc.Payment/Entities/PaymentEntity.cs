namespace Unisinos.Abc.Payment.Entities
{
    public class PaymentEntity
    {
        public Guid Id { get; set; }
        public Guid CorrelationId { get; set; }
        public string? Name { get; set; }
        public string? CPF { get; set; }
        public decimal TotalValue { get; set; }
        public int NumberInstallments { get; set; }
    }
}

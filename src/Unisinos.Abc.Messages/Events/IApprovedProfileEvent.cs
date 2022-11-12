namespace Unisinos.Abc.Messages.Events
{
    public interface IApprovedProfileEvent : IBaseEvent
    {
        Guid Id { get; set; }
        string? StudentName { get; set; }
        string? StudentCPF { get; set; }
        public decimal TotalValue { get; set; }
        public int NumberInstallments { get; set; }
        bool? IsApproved { get; set; }
    }
}

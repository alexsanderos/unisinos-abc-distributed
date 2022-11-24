namespace Unisinos.Abc.Messages.Events
{
    public interface IDisapprovedProfileEvent : IBaseEvent
    {
        string? StudentName { get; set; }
        string? StudentCPF { get; set; }
        bool? IsApproved { get; set; }
    }
}

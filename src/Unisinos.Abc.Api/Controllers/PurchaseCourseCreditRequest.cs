namespace Unisinos.Abc.Api.Controllers
{
    public class PurchaseCourseCreditRequest
    {
        public string? StudentName { get; set; }
        public string? StudentCPF { get; set; }
        public decimal TotalValue { get; set; }
        public int NumberInstallments { get; set; }
    }
}

namespace Unisinos.Abc.Api.Controllers
{
    public class PurchaseCourseCreditResponse
    {
        public PurchaseCourseCreditResponse(Guid correlationId)
        {
            CorrelationId = correlationId;
        }
        public Guid CorrelationId { get; set; }
    }
}

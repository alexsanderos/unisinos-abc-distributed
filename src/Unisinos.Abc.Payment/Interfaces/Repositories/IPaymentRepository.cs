using Unisinos.Abc.Payment.Entities;

namespace Unisinos.Abc.Payment.Interfaces.Repositories
{
    public interface IPaymentRepository
    {
        public void Add(PaymentEntity entity);
    }
}

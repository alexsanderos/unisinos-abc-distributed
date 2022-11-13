using Unisinos.Abc.Payment.Entities;
using Unisinos.Abc.Payment.Infrastructure.Data.Context;
using Unisinos.Abc.Payment.Interfaces.Repositories;

namespace Unisinos.Abc.Payment.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly UnisinosAbcContext _context;
        public PaymentRepository(UnisinosAbcContext context)
        {
            _context = context;
        }
        public void Add(PaymentEntity entity)
        {
            _context.Payments.Add(entity);
            _context.SaveChanges();
        }
    }
}

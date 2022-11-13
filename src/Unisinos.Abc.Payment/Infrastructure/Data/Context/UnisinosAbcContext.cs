using Microsoft.EntityFrameworkCore;
using Unisinos.Abc.Payment.Entities;
using Unisinos.Abc.Payment.Infrastructure.Data.Mappings;

namespace Unisinos.Abc.Payment.Infrastructure.Data.Context
{
    public class UnisinosAbcContext : DbContext
    {
        public UnisinosAbcContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<PaymentEntity> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PaymentEntityMap());
        }
    }
}

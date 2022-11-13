using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Unisinos.Abc.Payment.Entities;

namespace Unisinos.Abc.Payment.Infrastructure.Data.Mappings
{
    public class PaymentEntityMap : IEntityTypeConfiguration<PaymentEntity>
    {
        public void Configure(EntityTypeBuilder<PaymentEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CorrelationId)
                .IsRequired();

            builder.Property(x => x.Name)
                .IsRequired();

            builder.Property(x => x.CPF)
                .IsRequired();

            builder.Property(x => x.TotalValue)
                .HasColumnType("decimal(15,2)")
                .IsRequired();

            builder.Property(x => x.NumberInstallments)
                .IsRequired();
        }
    }
}

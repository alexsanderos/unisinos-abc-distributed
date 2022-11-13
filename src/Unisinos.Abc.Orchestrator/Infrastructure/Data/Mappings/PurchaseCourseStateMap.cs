using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Unisinos.Abc.Orchestrator.Sagas;

namespace Unisinos.Abc.Orchestrator.Infrastructure.Data.Mappings
{
    public class PurchaseCourseStateMap : SagaClassMap<PurchaseCourseState>
    {
        protected override void Configure(EntityTypeBuilder<PurchaseCourseState> entity, ModelBuilder model)
        {
            entity.HasKey(x => x.CorrelationId);
        }
    }
}

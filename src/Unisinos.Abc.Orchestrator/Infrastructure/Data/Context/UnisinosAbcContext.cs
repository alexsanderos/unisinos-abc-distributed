using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using Unisinos.Abc.Orchestrator.Infrastructure.Data.Mappings;

namespace Unisinos.Abc.Orchestrator.Infrastructure.Data.Context
{
    public class UnisinosAbcContext : SagaDbContext
    {
        public UnisinosAbcContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override IEnumerable<ISagaClassMap> Configurations
        {
            get { yield return new PurchaseCourseStateMap(); }
        }
    }
}

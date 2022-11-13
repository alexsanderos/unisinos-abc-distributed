using Microsoft.EntityFrameworkCore;
using Unisinos.Abc.Orchestrator.Infrastructure.Data.Context;

namespace Unisinos.Abc.Orchestrator.Infrastructure.Configuration
{
    public static class PersistenceConfiguration
    {
        public static IServiceCollection AddPersistenceConfiguration(
            this IServiceCollection services, IConfiguration configuration)
        {
            var sqlConnection = configuration.GetSection("ConnectionStrings:DatabaseConnection").Value;

            services.AddScoped<UnisinosAbcContext>();

            services.AddDbContext<DbContext, UnisinosAbcContext>((provider, builder) =>
            {
                builder.UseSqlServer(sqlConnection);
                builder.EnableSensitiveDataLogging(false);
            });

            return services;
        }
    }
}

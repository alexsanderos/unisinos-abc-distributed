using Microsoft.EntityFrameworkCore;
using Unisinos.Abc.Payment.Infrastructure.Data.Context;
using Unisinos.Abc.Payment.Infrastructure.Repositories;
using Unisinos.Abc.Payment.Interfaces.Repositories;

namespace Unisinos.Abc.Payment.Infrastructure.Configuration
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

            services.AddTransient<IPaymentRepository, PaymentRepository>();

            return services;
        }
    }
}

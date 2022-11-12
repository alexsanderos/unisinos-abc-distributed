using Microsoft.Extensions.DependencyInjection;
using Unisinos.Abc.Api.Application.Interfaces.Services;
using Unisinos.Abc.Api.Application.Services;

namespace Unisinos.Abc.Api.Application.Infrastructure.Configuration
{
    public static class ServicesConfiguration
    {
        public static void AddServicesConfirurations(this IServiceCollection services)
        {
            services.AddTransient<IPurchaseCourseService, PurchaseCourseService>();
        }
    }
}

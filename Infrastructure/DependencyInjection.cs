
using Application.Common.Interfaces.Services;
using Infrastructure.DbContext;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IApplicationDbContext, DapperDbContext>();
            return services;
        }
    }
}

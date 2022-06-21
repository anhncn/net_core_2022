
using Application.Common.Interfaces.Services;
using Infrastructure.DbContext;
using Infrastructure.Logs;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IApplicationDbContext, DapperDbContext>();
            services.AddTransient<ILogService, NLogService>();
            return services;
        }
    }
}

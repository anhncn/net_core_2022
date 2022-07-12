
using Application.Common.Interfaces.Services;
using Infrastructure.Cache;
using Infrastructure.DbContext.EntityFramework;
using Infrastructure.Logs;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<EntityframeWorkDbContext>();
            services.AddScoped<IApplicationDbContext, EFDbContext>();

            //services.AddScoped<IApplicationDbContext, DapperDbContext>();

            services.AddScoped<ILogService, NLogService>();

            //services.AddScoped<ICacheService, MemCacheService>();
            services.AddScoped<ICacheService, RedisCacheService>();

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost";
                options.InstanceName = "SampleInstance";
            });
            return services;
        }
    }
}

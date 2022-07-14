
using Application.Common.Interfaces.Infrastructure;
using Application.Common.Interfaces.Services;
using Infrastructure.Cache;
using Infrastructure.DbContext.EntityFramework;
using Infrastructure.Logs;
using Infrastructure.OTP;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<EntityframeWorkDbContext>();
            services.AddScoped<IApplicationDbContext, EFDbContext>();
            services.AddScoped<IOtpService, OTPService>();
            
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

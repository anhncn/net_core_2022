using Application.Common.Interfaces;
using Application.Common.Interfaces.WebUI;
using Domain.Common;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NNanh.Zolo.Services;
using System.Text;

namespace NNanh.Zolo
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddWebUI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthenJwtBearer(configuration);

            services
                .AddHealthChecks()
                .AddCheck<HealthCheckService>("health_check_radom")
                .AddRedis(configuration.Get<ApplicationSetting>().ConnectionStrings.Redis)
                .AddSqlServer(configuration.Get<ApplicationSetting>().ConnectionStrings.DefaultConnection);
            services.AddHealthChecksUI().AddInMemoryStorage();

            services.AddScoped<ITokenAuthService, TokenAuthService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }

        public static IServiceCollection AddAuthenJwtBearer(this IServiceCollection services, IConfiguration configuration)
        {
            var bytesKey = Encoding.ASCII.GetBytes(configuration.Get<ApplicationSetting>().Jwt.Key);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(bytesKey)
                };
            });
            return services;
        }


        public static IApplicationBuilder UseWebUI(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapHealthChecksUI();

            //    endpoints.MapHealthChecks("/health", new HealthCheckOptions
            //    {
            //        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            //    });

            //});

            return app;
        }
    }
}

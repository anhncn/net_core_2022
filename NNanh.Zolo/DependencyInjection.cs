using Application.Common.Interfaces;
using Application.Common.Interfaces.WebUI;
using Domain.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NNanh.Zolo.Services;
using System.Collections.Generic;
using System.Text;

namespace NNanh.Zolo
{
    /// <summary>
    /// swagger: https://www.c-sharpcorner.com/blogs/implement-swagger-in-asp-net-core-31-web-api
    /// </summary>
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebUI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthenJwtBearer(configuration);

            services
                .AddHealthChecks()
                .AddCheck<HealthCheckService>("health_check_radom")
                .AddRedis(configuration.Get<ApplicationSetting>().ConnectionStrings.Redis)
                .AddSqlServer(configuration.Get<ApplicationSetting>().ConnectionStrings.SqlServerDocker);
            services.AddHealthChecksUI().AddInMemoryStorage();

            services.AddSwaggerGen();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Implement Swagger UI",
                    Description = "A simple example to Implement Swagger UI",
                });
                // Include 'SecurityScheme' to use JWT Authentication
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, System.Array.Empty<string>() }
                });
            });

            services.AddScoped<ITokenAuthService, TokenAuthService>();
            services.AddScoped<IIDentityService, IDentityService>();
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
            app.UseSwagger();
            app.UseSwaggerUI(options => 
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Showing API V1");
            });

            return app;
        }
    }
}

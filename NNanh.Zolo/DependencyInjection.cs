﻿using Application.Common.Interfaces;
using Domain.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
    }
}

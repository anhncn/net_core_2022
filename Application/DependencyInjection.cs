using Application.BaseCommand;
using Application.Common.Behaviours;
using Application.Common.Interfaces.Application;
using Application.Common.Interfaces.Services;
using Application.Service;
using Application.Service.Interface;
using Application.ServiceBussiness;
using Application.ServiceBussiness.Implement;
using Domain;
using Domain.Common;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

            services.AddScoped(typeof(IDbService<>), typeof(DbService<>));
            services.AddScoped<IAppService, AppService>();

            services.AddScoped<IAccountService, AccountContextService>();

            services.AddRequestHandler<UserSql>();
            services.AddRequestHandler<Account>();

            return services;
        }

        public static void AddRequestHandler<T>(this IServiceCollection services)
        {
            services.AddTransient<IRequestHandler<BaseQueryCommand<T>, PaginatedList<T>>, BaseQueryCommandHandler<T>>();
            services.AddTransient<IRequestHandler<CreateBaseCommand<T>, ResponseResult>, CreateBaseCommandHandler<T>>();
            services.AddTransient<IRequestHandler<UpdateBaseCommand<T>, ResponseResult>, UpdateBaseCommandHandler<T>>();
            services.AddTransient<IRequestHandler<DeleteBaseCommand<T>, ResponseResult>, DeleteBaseCommandHandler<T>>();
        }
    }
}

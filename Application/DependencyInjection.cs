using Application.BaseCommand;
using Application.Common.Behaviours;
using Application.Common.Interfaces.Services;
using Application.Service;
using Domain;
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

            services.ConfigureServicesRequestHandler<TodoItem>();
            services.ConfigureServicesRequestHandler<UserInt>();

            return services;
        }

        public static void ConfigureServicesRequestHandler<T>(this IServiceCollection services)
        {
            services.ConfigureServicesQueryHandler<T>();
            services.ConfigureServicesCommandHandler<T>();
        }

        public static void ConfigureServicesQueryHandler<T>(this IServiceCollection services)
        {
            // BaseQuery<T> : IRequest<PaginatedList<T>>
            // BaseQueryHandler<T> : IRequestHandler<BaseQuery<T>, PaginatedList<T>>
            var typeT = typeof(T);
            var genericBaseRequest = typeof(BaseQuery<>).MakeGenericType(typeT);
            var genericPaginatedList = typeof(PaginatedList<>).MakeGenericType(typeT);
            var serviceType = typeof(IRequestHandler<,>).MakeGenericType(genericBaseRequest, genericPaginatedList);
            var implementType = typeof(BaseQueryHandler<>).MakeGenericType(typeT);
            services.AddScoped(serviceType, implementType);
        }

        public static void ConfigureServicesCommandHandler<T>(this IServiceCollection services)
        {
            // BaseCommand<T> : IRequest<bool>
            // BaseCommandHandler<T> : IRequestHandler<BaseCommand<T>, bool>
            var typeT = typeof(T);
            var genericBaseRequest = typeof(BaseCommand<>).MakeGenericType(typeT);
            var serviceType = typeof(IRequestHandler<,>).MakeGenericType(genericBaseRequest, typeof(bool));
            var implementType = typeof(BaseCommandHandler<>).MakeGenericType(typeT);
            services.AddScoped(serviceType, implementType);
        }
    }
}

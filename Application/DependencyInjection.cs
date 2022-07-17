using Application.BaseCommand;
using Application.Common.Behaviours;
using Application.Common.Interfaces.Application;
using Application.Common.Interfaces.Services;
using Application.Service;
using Application.Service.Interface;
using Application.ServiceBussiness;
using Application.ServiceBussiness.Implement;
using Domain.Common;
using Domain.Model;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
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
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
            services.AddTransient(typeof(IRequestPreProcessor<>), typeof(LoggingBehaviour<>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            services.AddScoped<IDbService, DbService>();
            services.AddScoped<IAppService, AppService>();

            services.AddScoped<IAccountService, AccountContextService>();

            services.AddRequestsHandler(AppDomain.CurrentDomain.GetAssemblies().First(a => a.FullName.Contains(nameof(Domain))));

            return services;
        }

        private static void AddRequestsHandler(this IServiceCollection services, Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));

            var types = assembly.GetExportedTypes().Where(t => t.BaseType == typeof(AuditableEntity));

            foreach (var type in types)
            {
                services.AddQueryHandler(type);
                services.AddCreateHandler(type);
                services.AddUpdateHandler(type);
                services.AddDeleteHandler(type);
            }
        }

        public static void AddQueryHandler(this IServiceCollection services, Type typeT)
        {
            var iRequest = typeof(BaseQueryCommand<>).MakeGenericType(typeT);
            var gPaginatedList = typeof(PaginatedList<>).MakeGenericType(typeT);
            var serviceType = typeof(IRequestHandler<,>).MakeGenericType(iRequest, gPaginatedList);
            var implementType = typeof(BaseQueryCommandHandler<>).MakeGenericType(typeT);
            services.AddTransient(serviceType, implementType);
        }

        public static void AddCreateHandler(this IServiceCollection services, Type typeT)
        {
            var iRequest = typeof(CreateBaseCommand<>).MakeGenericType(typeT);
            var serviceType = typeof(IRequestHandler<,>).MakeGenericType(iRequest, typeof(ResponseResultModel));
            var implementType = typeof(CreateBaseCommandHandler<>).MakeGenericType(typeT);
            services.AddTransient(serviceType, implementType);
        }

        public static void AddUpdateHandler(this IServiceCollection services, Type typeT)
        {
            var iRequest = typeof(UpdateBaseCommand<>).MakeGenericType(typeT);
            var serviceType = typeof(IRequestHandler<,>).MakeGenericType(iRequest, typeof(ResponseResultModel));
            var implementType = typeof(UpdateBaseCommandHandler<>).MakeGenericType(typeT);
            services.AddTransient(serviceType, implementType);
        }

        public static void AddDeleteHandler(this IServiceCollection services, Type typeT)
        {
            var iRequest = typeof(DeleteBaseCommand<>).MakeGenericType(typeT);
            var serviceType = typeof(IRequestHandler<,>).MakeGenericType(iRequest, typeof(ResponseResultModel));
            var implementType = typeof(DeleteBaseCommandHandler<>).MakeGenericType(typeT);
            services.AddTransient(serviceType, implementType);
        }


        //public static void AddRequestHandler<T>(this IServiceCollection services)
        //{
        //    services.AddTransient<IRequestHandler<BaseQueryCommand<T>, PaginatedList<T>>, BaseQueryCommandHandler<T>>();
        //    services.AddTransient<IRequestHandler<CreateBaseCommand<T>, ResponseResult>, CreateBaseCommandHandler<T>>();
        //    services.AddTransient<IRequestHandler<UpdateBaseCommand<T>, ResponseResult>, UpdateBaseCommandHandler<T>>();
        //    services.AddTransient<IRequestHandler<DeleteBaseCommand<T>, ResponseResult>, DeleteBaseCommandHandler<T>>();
        //}
    }
}

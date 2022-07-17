using Application.Common.Interfaces.Services;
using System;
using System.Linq;

namespace Application.Common.Mappings
{
    public static class MappingExtensions
    {
        public static PaginatedList<TDestination> PaginatedList<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize)
            => Application.PaginatedList<TDestination>.Create(queryable, pageNumber, pageSize);


        //public static IQueryable<TDestination> FilterSource<TDestination>(this IQueryable<TDestination> queryable, IDbService handler)
        //    where TDestination : Domain.Common.AuditableEntity
        //    => handler.Filter(queryable);

        //public static IQueryable<TDestination> FilterCommand<TDestination>(this IQueryable<TDestination> queryable, BaseQueryCommand<TDestination> handler)
        //    where TDestination : Domain.Common.AuditableEntity
        //    => handler.Filter(queryable);

        //public static IQueryable<TDestination> FilterSource<TDestination>(this IQueryable<TDestination> queryable, BaseQueryCommandHandler<TDestination> handler) 
        //    where TDestination : Domain.Common.AuditableEntity
        //    => handler.Filter(queryable);
        //public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable, IConfigurationProvider configuration)
        //    => queryable.ProjectTo<TDestination>(configuration).ToListAsync();
    }
}

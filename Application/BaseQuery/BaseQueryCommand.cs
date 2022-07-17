using Application.Common.Interfaces.Services;
using Application.Common.Mappings;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application
{
    public class PaginatedList<T>
    {
        public IEnumerable<T> Items { get; }
        public int PageIndex { get; }
        public int TotalPages { get; }
        public int TotalCount { get; }

        public PaginatedList(IEnumerable<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            Items = items;
        }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        public static PaginatedList<T> Create(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }

    public class BaseQueryCommand<T> : IRequest<PaginatedList<T>>
    {
        public IQueryable<T> _source;

        public int PageIndex { get; set; }
        public int PageSize { get; set; }

    }
    public class BaseQueryCommandHandler<TEntity> : IRequestHandler<BaseQueryCommand<TEntity>, PaginatedList<TEntity>>
        where TEntity : Domain.Common.AuditableEntity
    {
        protected readonly IDbService DbSerivce;
        public BaseQueryCommandHandler(IDbService dbContextService)
        {
            DbSerivce = dbContextService;
        }

        public Task<PaginatedList<TEntity>> Handle(BaseQueryCommand<TEntity> request, CancellationToken cancellationToken)
        {
            return DbSerivce.PaginatedList(request);
        }
    }

}

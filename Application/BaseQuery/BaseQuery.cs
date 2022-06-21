using Application.Common.Interfaces.Services;
using Application.Common.Mappings;
using Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }

    public class BaseQuery<T> : IRequest<PaginatedList<T>>
    {
        public string Title { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
    public class BaseQueryHandler<T> : IRequestHandler<BaseQuery<T>, PaginatedList<T>> where T : AuditableEntity
    {

        private readonly IDbService<T> _dbSerivce;
        public BaseQueryHandler(IDbService<T> dbContextService)
        {
            _dbSerivce = dbContextService;
        }

        public async Task<PaginatedList<T>> Handle(BaseQuery<T> request, CancellationToken cancellationToken)
        {
            var paging = _dbSerivce.AsQueryable().Where(x => true).PaginatedList(request.PageIndex, request.PageSize);

            await _dbSerivce.Context.SaveChangesAsync(cancellationToken);

            return paging;
        }
    }

}

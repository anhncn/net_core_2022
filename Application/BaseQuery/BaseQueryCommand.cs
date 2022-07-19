using Application.Common.Interfaces.Application;
using Application.Common.Interfaces.Services;
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

        public PaginatedList(IEnumerable<T> items, int pageIndex, int totalPages, int totalCount, bool active = false)
        {
            Items = items;
            PageIndex = pageIndex;
            TotalCount = totalCount;
            TotalPages = totalPages;
        }

        public PaginatedList<TEntity> Cast<TEntity>()
        {
            return new PaginatedList<TEntity>(Items.Cast<TEntity>(), PageIndex, TotalPages, TotalCount, false);
        }

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

    public partial class BaseQueryCommand<T> : IRequest<PaginatedList<T>>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

    }
    public partial class BaseQueryCommandHandler<TEntity> : IRequestHandler<BaseQueryCommand<TEntity>, PaginatedList<TEntity>>
        where TEntity : Domain.Common.AuditableEntity
    {

        private readonly IAppService _appService;

        private readonly IDbService _dbSerivce;

        protected IDbService DbSerivce => _dbSerivce;
        protected IAppService AppService => _appService;

        public BaseQueryCommandHandler(IDbService dbService, IAppService appService)
        {
            _dbSerivce = dbService;
            _appService = appService;
        }

        public Task<PaginatedList<TEntity>> Handle(BaseQueryCommand<TEntity> request, CancellationToken cancellationToken)
        {
            if (CommandsHandler.ContainsKey(typeof(TEntity)))
            {
                return CommandsHandler[typeof(TEntity)].Invoke(request, cancellationToken);
            }

            return DbSerivce.PaginatedList(request);
        }
    }

}

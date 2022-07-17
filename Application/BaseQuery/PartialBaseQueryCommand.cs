using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application
{
    public partial class BaseQueryCommandHandler<TEntity> where TEntity : Domain.Common.AuditableEntity
    {
        private IDictionary<Type, Func<BaseQueryCommand<TEntity>, CancellationToken, Task<PaginatedList<TEntity>>>> CommandsHandler => new
            Dictionary<Type, Func<BaseQueryCommand<TEntity>, CancellationToken, Task<PaginatedList<TEntity>>>>()
        {
            { typeof(Domain.Entities.Grade), GradeQueriesHandle }
        };
    }
}

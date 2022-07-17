using Domain.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services
{
    /// <summary>
    /// Xây dựng nghiệp vụ chung
    /// </summary>
    public interface IDbService
    {
        IApplicationDbContext Context { get; }
        Task<TEntity> FindAsync<TEntity>(string id) where TEntity : AuditableEntity; 

        Task<int> AddAsync<TEntity>(TEntity entity) where TEntity : AuditableEntity;

        Task<bool> UpdateAsync<TEntity>(TEntity entity) where TEntity : AuditableEntity;

        Task<bool> RemoveAsync<TEntity>(string id) where TEntity : AuditableEntity;

        IQueryable<TEntity> AsQueryable<TEntity>() where TEntity : AuditableEntity;
        Task<PaginatedList<TEntity>> PaginatedList<TEntity>(BaseQueryCommand<TEntity> request) where TEntity : AuditableEntity;

        IQueryable<TEntity> Filter<TEntity>(IQueryable<TEntity> source);

    }
}

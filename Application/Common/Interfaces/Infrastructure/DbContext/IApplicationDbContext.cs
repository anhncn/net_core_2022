using Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services
{
    /// <summary>
    /// Giao tiếp với Database
    /// </summary>
    public interface IApplicationDbContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : AuditableEntity;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

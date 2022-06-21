using Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services
{
    public interface IApplicationDbContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : AuditableEntity;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

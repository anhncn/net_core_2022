using Domain.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services
{
    public interface IDbContextService<T> where T : AuditableEntity
    {
        IApplicationDbContext Context { get; }
        Task<T> FindAsync(string id);

        Task<int> AddAsync(T entity);

        Task<bool> UpdateAsync(T entity);

        Task<bool> RemoveAsync(string id);

        IQueryable<T> AsQueryable();

    }
}

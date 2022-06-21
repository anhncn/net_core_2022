using System.Linq;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services
{
    public interface IDbSet<TEntity>
    {
        Task<TEntity> FindAsync(string id);

        IQueryable<TEntity> AsQueryable();

        Task<int> AddAsync(TEntity entity);

        Task<bool> UpdateAsync(TEntity entity);

        Task<bool> RemoveAsync(string id);
    }
}

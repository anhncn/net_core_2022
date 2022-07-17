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
        Task<T> FindAsync<T>(string id) where T : AuditableEntity; 

        Task<int> AddAsync<T>(T entity) where T : AuditableEntity;

        Task<bool> UpdateAsync<T>(T entity) where T : AuditableEntity;

        Task<bool> RemoveAsync<T>(string id) where T : AuditableEntity;

        IQueryable<T> AsQueryable<T>() where T : AuditableEntity;

    }
}

using Application.Common.Interfaces.Services;
using Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.DbContext.EntityFramework
{
    public class EFDbContext : IApplicationDbContext
    {
        private readonly Microsoft.EntityFrameworkCore.DbContext _dbContext;
        public EFDbContext(EntityframeWorkDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }

        public IDbSet<TEntity> Set<TEntity>() where TEntity : AuditableEntity
        {
            return new EFDbSet<TEntity>(_dbContext.Set<TEntity>());
        }
    }
}

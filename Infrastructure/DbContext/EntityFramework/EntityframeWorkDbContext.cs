using Application.Common.Interfaces.Services;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Infrastructure.DbContext.EntityFramework
{
    public class EntityframeWorkDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        private readonly ApplicationSetting _appSetting;

        public DbSet<UserSql> User { get; set; }
        public DbSet<Account> Account { get; set; }

        public EntityframeWorkDbContext(Microsoft.Extensions.Options.IOptions<ApplicationSetting> options)
        {
            _appSetting = options.Value;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(_appSetting.ConnectionStrings.SqlServerDocker);
        }
    }

    public class EFDbSet<TEntity> : IDbSet<TEntity> where TEntity : AuditableEntity
    {
        private readonly DbSet<TEntity> _dbSet;

        public EFDbSet(DbSet<TEntity> dbSet)
        {
            _dbSet = dbSet;
        }

        public Task<int> AddAsync(TEntity entity)
        {
            _dbSet.AddAsync(entity);

            return Task.FromResult(1);
        }

        public IQueryable<TEntity> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<TEntity> FindAsync(string id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<bool> RemoveAsync(string id)
        {
            _dbSet.Remove(await FindAsync(id));

            return true;
        }

        public Task<bool> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);

            return Task.FromResult(true);
        }
    }
}

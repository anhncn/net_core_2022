using Application.Common.Interfaces.Services;
using Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Service
{
    public class DbContextService<T> : IDbContextService<T> where T : AuditableEntity
    {
#pragma warning disable IDE1006 // Naming Styles

        private readonly IApplicationDbContext _context;
        private IDbSet<T> _dbSet { get; }
        public IApplicationDbContext Context { get { return _context; } }
#pragma warning restore IDE1006 // Naming Styles

        public DbContextService(IApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        #region Created

        public async Task<int> AddAsync(T entity)
        {
            int result = 0;

            await BeforeAdd(entity);

            if (await ValidateAdd(entity))
            {
                result = await _dbSet.AddAsync(entity);
            }

            await AfterAdd(entity);

            return result;
        }

        public virtual Task<bool> ValidateAdd(T entity)
        {
            return Task.FromResult(true);
        }

        public virtual Task BeforeAdd(T entity)
        {
            var props = typeof(T).GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(KeyAttribute)));
            if (props.Count() == 1)
            {
                if (props.First().PropertyType == typeof(string))
                {
                    props.First().SetValue(entity, Guid.NewGuid().ToString());
                }
                else if (props.First().PropertyType == typeof(Guid))
                {
                    props.First().SetValue(entity, Guid.NewGuid());
                }
            }
            entity.Created = DateTime.Now;
            entity.LastModified = DateTime.Now;
            return Task.CompletedTask;
        }

        public virtual Task AfterAdd(T entity)
        {
            return Task.CompletedTask;
        }

        #endregion


        public Task<bool> RemoveAsync(string id)
        {
            return _dbSet.RemoveAsync(id);
        }

        public IQueryable<T> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public Task<T> FindAsync(string id)
        {
            return _dbSet.FindAsync(id);
        }

        public Task<bool> UpdateAsync(T entity)
        {
            return _dbSet.UpdateAsync(entity);
        }
    }
}

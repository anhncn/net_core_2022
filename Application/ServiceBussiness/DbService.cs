using Application.Common.Interfaces;
using Application.Common.Interfaces.Services;
using Application.Common.Mappings;
using Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Service
{
    public class DbService : IDbService
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserService _userService;

        public IApplicationDbContext Context => _context;

        public DbService(IApplicationDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        #region Created

        public async Task<int> AddAsync<TEntity>(TEntity entity) where TEntity : AuditableEntity
        {
            int result = 0;

            await BeforeAdd(entity);

            if (await ValidateAdd(entity))
            {
                result = await Context.Set<TEntity>().AddAsync(entity);
            }

            await AfterAdd(entity);

            return result;
        }
         
        public virtual Task<bool> ValidateAdd<TEntity>(TEntity entity) where TEntity : AuditableEntity
        {
            return Task.FromResult(true);
        }

        public virtual Task BeforeAdd<TEntity>(TEntity entity) where TEntity : AuditableEntity
        {
            var props = typeof(TEntity).GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(KeyAttribute)));
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
            entity.CreatedBy = _userService.UserName;
            entity.LastModified = DateTime.Now;
            return Task.CompletedTask;
        }

        public virtual Task AfterAdd<TEntity>(TEntity entity) where TEntity : AuditableEntity
        {
            return Task.CompletedTask;
        }

        #endregion


        public Task<bool> RemoveAsync<TEntity>(string id) where TEntity : AuditableEntity
        {
            return Context.Set<TEntity>().RemoveAsync(id);
        }

        public IQueryable<TEntity> AsQueryable<TEntity>() where TEntity : AuditableEntity
        {
            return Context.Set<TEntity>().AsQueryable();
        }

        public Task<TEntity> FindAsync<TEntity>(string id) where TEntity : AuditableEntity
        {
            return Context.Set<TEntity>().FindAsync(id);
        }

        public Task<bool> UpdateAsync<TEntity>(TEntity entity) where TEntity : AuditableEntity
        {
            return Context.Set<TEntity>().UpdateAsync(entity);
        }

        public Task<PaginatedList<TEntity>> PaginatedList<TEntity>(BaseQueryCommand<TEntity> request) where TEntity : AuditableEntity
        {
            var paging = AsQueryable<TEntity>().PaginatedList(request.PageIndex, request.PageSize);

            return Task.FromResult(paging);
        }

        public IQueryable<TEntity> Filter<TEntity>(IQueryable<TEntity> source)
        {
            return source;
        }

    }
}

using Application.Common.Interfaces;
using Application.Common.Interfaces.Services;
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

        public async Task<int> AddAsync<T>(T entity) where T : AuditableEntity
        {
            int result = 0;

            await BeforeAdd(entity);

            if (await ValidateAdd(entity))
            {
                result = await Context.Set<T>().AddAsync(entity);
            }

            await AfterAdd(entity);

            return result;
        }
         
        public virtual Task<bool> ValidateAdd<T>(T entity) where T : AuditableEntity
        {
            return Task.FromResult(true);
        }

        public virtual Task BeforeAdd<T>(T entity) where T : AuditableEntity
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
            entity.CreatedBy = _userService.UserId;
            entity.LastModified = DateTime.Now;
            return Task.CompletedTask;
        }

        public virtual Task AfterAdd<T>(T entity) where T : AuditableEntity
        {
            return Task.CompletedTask;
        }

        #endregion


        public Task<bool> RemoveAsync<T>(string id) where T : AuditableEntity
        {
            return Context.Set<T>().RemoveAsync(id);
        }

        public IQueryable<T> AsQueryable<T>() where T : AuditableEntity
        {
            return Context.Set<T>().AsQueryable();
        }

        public Task<T> FindAsync<T>(string id) where T : AuditableEntity
        {
            return Context.Set<T>().FindAsync(id);
        }

        public Task<bool> UpdateAsync<T>(T entity) where T : AuditableEntity
        {
            return Context.Set<T>().UpdateAsync(entity);
        }
    }
}

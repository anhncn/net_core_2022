using Application.Common.Interfaces;
using Application.Common.Interfaces.Services;
using Application.Common.Mappings;
using Application.ServiceBussiness;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Service
{
    public class DbService : IDbService
    {
        private IUserService UserService { get; }

        public IApplicationDbContext Context { get; }
        private DbProcessService ProcessService { get; }

        public DbService(IApplicationDbContext context, IUserService userService, DbProcessService process)
        {
            Context = context;
            UserService = userService;
            ProcessService = process;
        }

        #region Created

        public async Task<int> CreateAsync<TEntity>(TEntity entity) where TEntity : AuditableEntity
        {
            int result = 0;

            await BeforeCreate(entity);

            if (await ProcessService.CreateValidator(entity))
            {
                result = await Context.Set<TEntity>().AddAsync(entity);
            }

            await AfterCreated(entity);

            return result;
        }

        public virtual Task BeforeCreate<TEntity>(TEntity entity) where TEntity : AuditableEntity
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
            entity.CreatedBy = UserService.UserId;
            entity.LastModified = DateTime.Now;
            return Task.CompletedTask;
        }

        public virtual Task AfterCreated<TEntity>(TEntity entity) where TEntity : AuditableEntity
        {
            return Task.CompletedTask;
        }

        #endregion

        #region Update

        public async Task<bool> UpdateAsync<TEntity>(TEntity entity) where TEntity : AuditableEntity
        {
            bool result = true;

            await BeforeUpdate(entity);

            if (await ProcessService.UpdateValidator(entity))
            {
                result = await Context.Set<TEntity>().UpdateAsync(entity);
            }

            await AfterUpdated(entity);

            return result;
        }

        public virtual Task BeforeUpdate<TEntity>(TEntity entity) where TEntity : AuditableEntity
        {
            entity.LastModified = DateTime.Now;
            entity.LastModifiedBy = UserService.UserName;
            return Task.CompletedTask;
        }

        public virtual Task AfterUpdated<TEntity>(TEntity entity) where TEntity : AuditableEntity
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


        public Task<PaginatedList<TEntity>> PaginatedList<TEntity>(BaseQueryCommand<TEntity> request) where TEntity : AuditableEntity
        {
            var paging = AsQueryable<TEntity>().PaginatedList(request.PageIndex, request.PageSize);

            return Task.FromResult(paging);
        }
    }
}

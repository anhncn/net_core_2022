
using Application.Common.Interfaces.Services;
using Domain.Common;
using Domain.Enumerations;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BaseCommand
{
    public abstract class BaseCommand<TEntity>
    {
        public string Id { get; set; }
        public TEntity Entity { get; set; }
    }

    #region Create
    public class CreateBaseCommand<TEntity> : BaseCommand<TEntity>, IRequest<bool>
    {

    }
    public class CreateBaseCommandHandler<TEntity> : IRequestHandler<CreateBaseCommand<TEntity>, bool> where TEntity : AuditableEntity
    {
        private readonly IDbService<TEntity> _dbService;
        public CreateBaseCommandHandler(IDbService<TEntity> dbService)
        {
            _dbService = dbService;
        }

        public virtual async Task<bool> Handle(CreateBaseCommand<TEntity> request, CancellationToken cancellationToken)
        {
            await _dbService.AddAsync(request.Entity);
            await _dbService.Context.SaveChangesAsync(cancellationToken);
            return false;
        }
    }

    #endregion

    #region Update
    public class UpdateBaseCommand<TEntity> : BaseCommand<TEntity>, IRequest<bool>
    {

    }
    public class UpdateBaseCommandHandler<TEntity> : IRequestHandler<UpdateBaseCommand<TEntity>, bool> where TEntity : AuditableEntity
    {
        private readonly IDbService<TEntity> _dbService;
        public UpdateBaseCommandHandler(IDbService<TEntity> dbService)
        {
            _dbService = dbService;
        }

        public virtual async Task<bool> Handle(UpdateBaseCommand<TEntity> request, CancellationToken cancellationToken)
        {
            await _dbService.UpdateAsync(request.Entity);
            await _dbService.Context.SaveChangesAsync(cancellationToken);

            return false;
        }
    }

    #endregion

    #region Delete
    public class DeleteBaseCommand<TEntity> : BaseCommand<TEntity>, IRequest<bool>
    {

    }
    public class DeleteBaseCommandHandler<TEntity> : IRequestHandler<DeleteBaseCommand<TEntity>, bool> where TEntity : AuditableEntity
    {
        private readonly IDbService<TEntity> _dbService;
        public DeleteBaseCommandHandler(IDbService<TEntity> dbService)
        {
            _dbService = dbService;
        }

        public virtual async Task<bool> Handle(DeleteBaseCommand<TEntity> request, CancellationToken cancellationToken)
        {
            await _dbService.RemoveAsync(request.Id);
            await _dbService.Context.SaveChangesAsync(cancellationToken);
            return false;
        }
    }

    #endregion

}

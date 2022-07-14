using Application.Common.Interfaces.Services;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BaseCommand
{
    public class UpdateBaseCommand<TEntity> : IBaseCommand 
    {
        public TEntity Entity { get; set; }

        private UpdateBaseCommand(TEntity entity)
        {
            Entity = entity;
        }

        public static UpdateBaseCommand<TEntity> Instance(TEntity entity)
        {
            return new UpdateBaseCommand<TEntity>(entity);
        }
    }
    public class UpdateBaseCommandHandler<TEntity> : BaseCommandHandler<TEntity, UpdateBaseCommand<TEntity>>
    {
        public UpdateBaseCommandHandler(IDbService<TEntity> dbService) : base(dbService) { }
        public override async Task<ResponseResultModel> Handle(UpdateBaseCommand<TEntity> request, CancellationToken cancellationToken)
        {
            await DbService.UpdateAsync(request.Entity);
            await DbService.Context.SaveChangesAsync(cancellationToken);

            return default;
        }
    }
}

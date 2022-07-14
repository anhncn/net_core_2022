using Application.Common.Interfaces.Services;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;
namespace Application.BaseCommand
{
    public class DeleteBaseCommand<TEntity> : BaseExcuteCommand<TEntity> { }
    public class DeleteBaseCommandHandler<TEntity> : BaseExcuteCommandHandler<TEntity, DeleteBaseCommand<TEntity>>
    {
        public DeleteBaseCommandHandler(IDbService<TEntity> dbService) : base(dbService) { }

        public override async Task<ResponseResultModel> Handle(DeleteBaseCommand<TEntity> request, CancellationToken cancellationToken)
        {
            await DbService.RemoveAsync(request.Id);
            await DbService.Context.SaveChangesAsync(cancellationToken);
            return default;
        }
    }
}

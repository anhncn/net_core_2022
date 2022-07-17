using Application.Common.Interfaces.Services;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;
namespace Application.BaseCommand
{
    public class DeleteBaseCommand<TEntity> : IBaseCommand
    {
        public string Id { get; set; }
    }
    public class DeleteBaseCommandHandler<TEntity> : BaseCommandHandler<TEntity, DeleteBaseCommand<TEntity>>
        where TEntity : Domain.Common.AuditableEntity
    {
        public DeleteBaseCommandHandler(IDbService dbService) : base(dbService) { }

        public override async Task<ResponseResultModel> Handle(DeleteBaseCommand<TEntity> request, CancellationToken cancellationToken)
        {
            await DbService.RemoveAsync<TEntity>(request.Id);
            await DbService.Context.SaveChangesAsync(cancellationToken);
            return default;
        }
    }
}

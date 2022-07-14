using Application.Common.Interfaces.Services;
using Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BaseCommand
{
    public class CreateBaseCommand<TEntity> : BaseExcuteCommand<TEntity> { }

    public class CreateBaseCommandHandler<TEntity> : BaseExcuteCommandHandler<TEntity, CreateBaseCommand<TEntity>> 
    {
        public CreateBaseCommandHandler(IDbService<TEntity> dbService) : base(dbService) { }

        public override async Task<ResponseResult> Handle(CreateBaseCommand<TEntity> request, CancellationToken cancellationToken)
        {
            await DbService.AddAsync(request.Entity);
            await DbService.Context.SaveChangesAsync(cancellationToken);
            return default;
        }
    }
}

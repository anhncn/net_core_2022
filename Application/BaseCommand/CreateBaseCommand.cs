using Application.Common.Interfaces.Services;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BaseCommand
{
    public class CreateBaseCommand<TEntity> : IBaseCommand
    {
        public TEntity Entity { get; set; }

        private CreateBaseCommand(TEntity entity)
        {
            Entity = entity;
        }

        public static CreateBaseCommand<TEntity> Instance(TEntity entity = default)
        {
            return new CreateBaseCommand<TEntity>(entity);
        }
    }

    public class CreateBaseCommandHandler<TEntity> : BaseCommandHandler<TEntity, CreateBaseCommand<TEntity>>
        where TEntity : Domain.Common.AuditableEntity
    {
        public CreateBaseCommandHandler(IDbService dbService) : base(dbService) { }

        public override async Task<ResponseResultModel> Handle(CreateBaseCommand<TEntity> request, CancellationToken cancellationToken)
        {
            await DbService.AddAsync(request.Entity);
            await DbService.Context.SaveChangesAsync(cancellationToken);
            return default;
        }
    }
}

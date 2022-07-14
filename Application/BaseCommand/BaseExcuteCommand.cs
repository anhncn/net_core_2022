using Application.Common.Interfaces.Services;

namespace Application.BaseCommand
{
    public abstract class BaseExcuteCommand<TEntity> : BaseCommand
    {
        public string Id { get; set; }
        public TEntity Entity { get; set; }
    }

    public abstract class BaseExcuteCommandHandler<TEntity, TCommand> : BaseCommandHandler<TEntity, TCommand>
        where TCommand : BaseExcuteCommand<TEntity>
    {
        public BaseExcuteCommandHandler(IDbService<TEntity> dbService) : base(dbService) { }

    }
}

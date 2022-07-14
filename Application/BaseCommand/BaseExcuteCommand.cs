using Application.Common.Interfaces.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BaseCommand
{
    public abstract class BaseCommand : IRequest<ResponseResult> { }

    public abstract class BaseCommandHandler<TEntity, TCommand> : IRequestHandler<TCommand, ResponseResult>
        where TCommand : BaseCommand
    {
        protected readonly IDbService<TEntity> DbService;
        public BaseCommandHandler(IDbService<TEntity> dbService) { DbService = dbService; }

        public abstract Task<ResponseResult> Handle(TCommand request, CancellationToken cancellationToken);
    }

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

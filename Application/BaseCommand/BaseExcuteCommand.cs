using Application.Common.Interfaces.Services;
using Domain.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BaseCommand
{
    public abstract class BaseExcuteCommand<TEntity> : IRequest<ResponseResult> 
    {
        public string Id { get; set; }
        public TEntity Entity { get; set; }
    }

    public abstract class BaseCommandHandler<TEntity, TCommand> : IRequestHandler<TCommand, ResponseResult>
        where TCommand : BaseExcuteCommand<TEntity>
    {
        protected readonly IDbService<TEntity> DbService;
        public BaseCommandHandler(IDbService<TEntity> dbService)
        {
            DbService = dbService;
        }

        public abstract Task<ResponseResult> Handle(TCommand request, CancellationToken cancellationToken);
    }
}

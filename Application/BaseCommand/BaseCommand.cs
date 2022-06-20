
using Application.Common.Interfaces.Services;
using Domain.Common;
using Domain.Enumerations;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BaseCommand
{
    public class BaseCommand<T> : IRequest<bool>
    {
        public EntityState EntityState { get; set; } = EntityState.None;

        public string Id { get; set; }

        public T Entity { get; set; }
    }
    public class BaseCommandHandler<T> : IRequestHandler<BaseCommand<T>, bool> where T : AuditableEntity
    {

        private readonly IDbContextService<T> _dbContextService;
        public BaseCommandHandler(IDbContextService<T> dbContextService)
        {
            _dbContextService = dbContextService;
        }

        public async Task<bool> Handle(BaseCommand<T> request, CancellationToken cancellationToken)
        {
            switch (request.EntityState)
            {
                case EntityState.Created:
                    await _dbContextService.AddAsync(request.Entity);
                    await _dbContextService.Context.SaveChangesAsync(cancellationToken);
                    break;
                case EntityState.Updated:
                    await _dbContextService.UpdateAsync(request.Entity);
                    await _dbContextService.Context.SaveChangesAsync(cancellationToken);
                    break;
                case EntityState.Deleted:
                    await _dbContextService.RemoveAsync(request.Id);
                    await _dbContextService.Context.SaveChangesAsync(cancellationToken);
                    break;
                default:
                case EntityState.None:
                    break;
            }
            return false;
        }
    }
}

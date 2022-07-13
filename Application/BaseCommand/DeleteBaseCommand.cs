﻿using Application.Common.Interfaces.Services;
using Domain.Common;
using System.Threading;
using System.Threading.Tasks;
namespace Application.BaseCommand
{
    public class DeleteBaseCommand<TEntity> : BaseExcuteCommand<TEntity> { }
    public class DeleteBaseCommandHandler<TEntity> : BaseCommandHandler<TEntity, DeleteBaseCommand<TEntity>> where TEntity : AuditableEntity
    {
        public DeleteBaseCommandHandler(IDbService<TEntity> dbService) : base(dbService) { }

        public override async Task<ResponseResult> Handle(DeleteBaseCommand<TEntity> request, CancellationToken cancellationToken)
        {
            await DbService.RemoveAsync(request.Id);
            await DbService.Context.SaveChangesAsync(cancellationToken);
            return default;
        }
    }
}

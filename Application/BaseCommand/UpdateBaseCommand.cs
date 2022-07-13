﻿using Application.Common.Interfaces.Services;
using Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BaseCommand
{
    public class UpdateBaseCommand<TEntity> : BaseExcuteCommand<TEntity> { }
    public class UpdateBaseCommandHandler<TEntity> : BaseCommandHandler<TEntity, UpdateBaseCommand<TEntity>>
    {
        public UpdateBaseCommandHandler(IDbService<TEntity> dbService) : base(dbService) { }
        public override async Task<ResponseResult> Handle(UpdateBaseCommand<TEntity> request, CancellationToken cancellationToken)
        {
            await DbService.UpdateAsync(request.Entity);
            await DbService.Context.SaveChangesAsync(cancellationToken);

            return default;
        }
    }
}
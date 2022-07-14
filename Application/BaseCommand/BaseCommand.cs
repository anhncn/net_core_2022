﻿using Application.Common.Interfaces.Services;
using Domain.Model;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.BaseCommand
{
    public interface IBaseCommand : IRequest<ResponseResultModel> { }

    public abstract class BaseCommandHandler<TEntity, TCommand> : IRequestHandler<TCommand, ResponseResultModel>
        where TCommand : IBaseCommand
    {
        protected readonly IDbService<TEntity> DbService;
        public BaseCommandHandler(IDbService<TEntity> dbService) { DbService = dbService; }
        public abstract Task<ResponseResultModel> Handle(TCommand request, CancellationToken cancellationToken);
    }
}

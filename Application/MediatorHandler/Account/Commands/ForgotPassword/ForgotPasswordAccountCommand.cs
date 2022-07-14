﻿using Application.BaseCommand;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MediatorHandler.Account.Commands.ForgotPassword
{
    public class ForgotPasswordAccountCommand : BaseCommand.IBaseCommand
    {
        public string UserName { get; set; }
    }

    public class ForgotPasswordAccountCommandHandler : BaseCommandHandler<Domain.Entities.Account, ForgotPasswordAccountCommand>
    {
        private new Service.Interface.IAccountService DbService => base.DbService as Service.Interface.IAccountService;
        public ForgotPasswordAccountCommandHandler(Service.Interface.IAccountService dbService) : base(dbService) { }

        public override Task<ResponseResultModel> Handle(ForgotPasswordAccountCommand request, CancellationToken cancellationToken)
        {
            return DbService.ForgotPassword(request);
        }
    }
}

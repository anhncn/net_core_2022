using Application.BaseCommand;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MediatorHandler.Account.Commands.ForgotPassword
{
    public class ForgotPasswordOtpResendAccountCommand : ForgotPasswordAccountCommand { }

    public class ForgotPasswordOtpResendAccountCommandHandler : BaseCommandHandler<Domain.Entities.Account, ForgotPasswordOtpResendAccountCommand>
    {
        private new Service.Interface.IAccountService DbService => base.DbService as Service.Interface.IAccountService;

        public ForgotPasswordOtpResendAccountCommandHandler(Service.Interface.IAccountService dbService) : base(dbService) { }

        public override Task<ResponseResult> Handle(ForgotPasswordOtpResendAccountCommand request, CancellationToken cancellationToken)
        {
            return DbService.ForgotPasswordOtpResend(request);
        }
    }
}

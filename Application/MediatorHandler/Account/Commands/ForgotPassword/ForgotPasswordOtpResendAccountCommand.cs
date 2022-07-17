using Application.BaseCommand;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MediatorHandler.Account.Commands.ForgotPassword
{
    public class ForgotPasswordOtpResendAccountCommand : ForgotPasswordAccountCommand { }

    public class ForgotPasswordOtpResendAccountCommandHandler : BaseCommandHandler<Domain.Entities.Account, ForgotPasswordOtpResendAccountCommand>
    {
        private readonly Service.Interface.IAccountService _accountService;

        public ForgotPasswordOtpResendAccountCommandHandler(Service.Interface.IAccountService accountService) { _accountService = accountService; }

        public override Task<ResponseResultModel> Handle(ForgotPasswordOtpResendAccountCommand request, CancellationToken cancellationToken)
        {
            return _accountService.ForgotPasswordOtpResend(request);
        }
    }
}

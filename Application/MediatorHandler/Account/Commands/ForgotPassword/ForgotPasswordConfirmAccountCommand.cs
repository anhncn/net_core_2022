using Application.BaseCommand;
using System.Threading;
using System.Threading.Tasks;
using Domain.Model;

namespace Application.MediatorHandler.Account.Commands.ForgotPassword
{
    public class ForgotPasswordConfirmAccountCommand : ForgotPasswordAccountCommand
    {
        public string OTPValue { get; set; }
    }

    public class ForgotPasswordConfirmAccountCommandHandler : BaseCommandHandler<Domain.Entities.Account, ForgotPasswordConfirmAccountCommand>
    {
        private readonly Service.Interface.IAccountService _accountService;

        public ForgotPasswordConfirmAccountCommandHandler(Service.Interface.IAccountService accountService) { _accountService = accountService; }

        public override Task<ResponseResultModel> Handle(ForgotPasswordConfirmAccountCommand request, CancellationToken cancellationToken)
        {
            return _accountService.ForgotPasswordConfirm(request);
        }
    }
}

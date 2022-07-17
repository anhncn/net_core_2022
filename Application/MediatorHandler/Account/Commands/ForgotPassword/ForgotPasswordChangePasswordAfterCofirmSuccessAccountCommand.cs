using Application.BaseCommand;
using Domain.Model;

namespace Application.MediatorHandler.Account.Commands.ForgotPassword
{
    public class ForgotPasswordChangePasswordAfterCofirmSuccessAccountCommand : ForgotPasswordAccountCommand
    {
        public string OTPValue { get; set; }
        public string PasswordNew { get; set; }
        public string PasswordConfirm { get; set; }
    }


    public class ForgotPasswordChangePasswordAfterCofirmSuccessAccountCommandHandler : BaseCommandHandler<Domain.Entities.Account, ForgotPasswordChangePasswordAfterCofirmSuccessAccountCommand>
    {
        private readonly Service.Interface.IAccountService _accountService;

        public ForgotPasswordChangePasswordAfterCofirmSuccessAccountCommandHandler(Service.Interface.IAccountService accountService) 
        { 
            _accountService = accountService; 
        }

        public override System.Threading.Tasks.Task<ResponseResultModel> Handle(ForgotPasswordChangePasswordAfterCofirmSuccessAccountCommand request, System.Threading.CancellationToken cancellationToken)
        {
            return _accountService.ForgotPasswordChangePasswordAfterCofirmSuccess(request);
        }
    }
}

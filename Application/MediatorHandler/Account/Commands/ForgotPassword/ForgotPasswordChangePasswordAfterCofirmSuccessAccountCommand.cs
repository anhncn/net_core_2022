using Application.BaseCommand;

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
        private new Service.Interface.IAccountService DbService => base.DbService as Service.Interface.IAccountService;

        public ForgotPasswordChangePasswordAfterCofirmSuccessAccountCommandHandler(Service.Interface.IAccountService dbService) : base(dbService){}

        public override System.Threading.Tasks.Task<ResponseResult> Handle(ForgotPasswordChangePasswordAfterCofirmSuccessAccountCommand request, System.Threading.CancellationToken cancellationToken)
        {
            return DbService.ForgotPasswordChangePasswordAfterCofirmSuccess(request);
        }
    }
}

using Application.BaseCommand;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MediatorHandler.Account.Commands.ForgotPassword
{
    public class ForgotPasswordConfirmAccountCommand : ForgotPasswordAccountCommand
    {
        public string OTPValue { get; set; }
    }

    public class ForgotPasswordConfirmAccountCommandHandler : BaseCommandHandler<Domain.Entities.Account, ForgotPasswordConfirmAccountCommand>
    {
        private new Service.Interface.IAccountService DbService => base.DbService as Service.Interface.IAccountService;

        public ForgotPasswordConfirmAccountCommandHandler(Service.Interface.IAccountService dbService) : base(dbService) { }

        public override Task<ResponseResult> Handle(ForgotPasswordConfirmAccountCommand request, CancellationToken cancellationToken)
        {
            return DbService.ForgotPasswordConfirm(request);
        }
    }
}

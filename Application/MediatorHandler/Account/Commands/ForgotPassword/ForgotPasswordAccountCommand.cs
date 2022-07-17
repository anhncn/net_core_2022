using Application.BaseCommand;
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
        private readonly Service.Interface.IAccountService _accountService;
        public ForgotPasswordAccountCommandHandler(Service.Interface.IAccountService accountService) { _accountService = accountService; }

        public override Task<ResponseResultModel> Handle(ForgotPasswordAccountCommand request, CancellationToken cancellationToken)
        {
            return _accountService.ForgotPassword(request);
        }
    }
}

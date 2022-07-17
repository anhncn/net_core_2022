using Domain.Model;
using Application.BaseCommand;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MediatorHandler.Account.Commands
{
    public class LoginAccountCommand : BaseCommand.IBaseCommand
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }


    public class LoginAccountCommandHandler : BaseCommandHandler<Domain.Entities.Account, LoginAccountCommand>
    {
        private readonly Service.Interface.IAccountService _accountService;

        public LoginAccountCommandHandler(Service.Interface.IAccountService dbService) { _accountService = dbService; }

        public override Task<ResponseResultModel> Handle(LoginAccountCommand request, CancellationToken cancellationToken)
        {
            return _accountService.LoginAsync(request);
        }
    }

}

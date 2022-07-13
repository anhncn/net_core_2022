
using Application.BaseCommand;
using Application.Service.Interface;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MediatorHandler.Account.Commands
{
    public class LoginAccountCommand : BaseExcuteCommand<Domain.Entities.Account> { }


    public class LoginAccountCommandHandler : BaseCommandHandler<Domain.Entities.Account, LoginAccountCommand>
    {
        private readonly IAccountService _accountService;

        public LoginAccountCommandHandler(IAccountService dbService) : base(dbService)
        {
            _accountService = dbService;
        }

        public override Task<ResponseResult> Handle(LoginAccountCommand request, CancellationToken cancellationToken)
        {
            return _accountService.LoginAsync(request.Entity);
        }
    }

}

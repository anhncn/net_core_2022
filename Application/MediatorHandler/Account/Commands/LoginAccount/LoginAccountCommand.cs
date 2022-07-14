
using Application.BaseCommand;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MediatorHandler.Account.Commands
{
    public class LoginAccountCommand : BaseCommand.BaseCommand
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }


    public class LoginAccountCommandHandler : BaseCommandHandler<Domain.Entities.Account, LoginAccountCommand>
    {
        private new Service.Interface.IAccountService DbService => base.DbService as Service.Interface.IAccountService;

        public LoginAccountCommandHandler(Service.Interface.IAccountService dbService) : base(dbService){}

        public override Task<ResponseResult> Handle(LoginAccountCommand request, CancellationToken cancellationToken)
        {
            return DbService.LoginAsync(request);
        }
    }

}

using Application.BaseCommand;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MediatorHandler.Account.Commands
{
    public class RegisterAccountCommand : BaseCommand.IBaseCommand
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }


    public class RegisterAccountCommandHandler : BaseCommandHandler<Domain.Entities.Account, RegisterAccountCommand>
    {
        private readonly Service.Interface.IAccountService _accountService;
        public RegisterAccountCommandHandler(Service.Interface.IAccountService accountService)
        {
            _accountService = accountService;
        }

        public override Task<ResponseResultModel> Handle(RegisterAccountCommand request, CancellationToken cancellationToken)
        {
            return _accountService.RegisterAsync(request);
        }
    }
}

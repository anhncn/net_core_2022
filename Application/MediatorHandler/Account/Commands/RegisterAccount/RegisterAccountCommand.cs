using Application.BaseCommand;
using Application.Common.Interfaces.Services;
using Application.Service.Interface;
using System.Threading;
using System.Threading.Tasks;

namespace Application.MediatorHandler.Account.Commands
{
    public class RegisterAccountCommand : BaseExcuteCommand<Domain.Entities.Account> { }


    public class RegisterAccountCommandHandler : BaseCommandHandler<Domain.Entities.Account, RegisterAccountCommand>
    {
        private readonly IAccountService _accountService;
        public RegisterAccountCommandHandler(IAccountService dbService) : base(dbService) { _accountService = dbService; }

        public override Task<ResponseResult> Handle(RegisterAccountCommand request, CancellationToken cancellationToken)
        {
            return _accountService.RegisterAsync(request.Entity);
        }
    }
}

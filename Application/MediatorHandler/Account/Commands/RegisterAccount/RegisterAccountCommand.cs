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
    }


    public class RegisterAccountCommandHandler : BaseCommandHandler<Domain.Entities.Account, RegisterAccountCommand>
    {
        private new Service.Interface.IAccountService DbService => base.DbService as Service.Interface.IAccountService;
        public RegisterAccountCommandHandler(Service.Interface.IAccountService dbService) : base(dbService) { }

        public override Task<ResponseResultModel> Handle(RegisterAccountCommand request, CancellationToken cancellationToken)
        {
            return DbService.RegisterAsync(request);
        }
    }
}

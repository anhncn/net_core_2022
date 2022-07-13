using Application.BaseCommand;
using Application.MediatorHandler.Account.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace NNanh.Zolo.Controllers
{
    public class AccountsController : ApiControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<ResponseResult>> Login(LoginAccountCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("register")]
        public async Task<ActionResult<ResponseResult>> Register(RegisterAccountCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}


using Application;
using Application.BaseCommand;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;

namespace NNanh.Zolo.Controllers
{
    [Authorize]
    public class UsersController : ApiControllerBase
    {
        private readonly ILogService _logger;

        public UsersController(ILogService logger)
        {
            _logger = logger;
            _logger.Info("NLog injected into UsersController");
        }


        [HttpGet]
        public async Task<ActionResult<PaginatedList<UserInt>>> GetWithPagination([FromQuery] BaseQuery<UserInt> query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Create(BaseCommand<UserInt> command)
        {
            return await Mediator.Send(command);
        }
    }
}

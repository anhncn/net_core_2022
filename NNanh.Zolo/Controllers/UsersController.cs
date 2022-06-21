
using Application;
using Application.BaseCommand;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace NNanh.Zolo.Controllers
{
    public class UsersController : ApiControllerBase
    {
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;
            _logger.LogInformation(1, "NLog injected into HomeController");
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

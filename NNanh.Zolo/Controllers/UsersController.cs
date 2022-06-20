
using Application;
using Application.BaseCommand;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace NNanh.Zolo.Controllers
{
    public class UsersController : ApiControllerBase
    {
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

using Application;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NNanh.Zolo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedList<TodoItem>>> GetTodoItemsWithPagination([FromQuery] BaseQueryCommand<TodoItem> query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public IActionResult Send([FromForm] EmailModel emailModel)
        {

            emailModel.Files = null;
            return Ok(JsonConvert.SerializeObject(emailModel));
        }
    }

    public class EmailModel
    {
        public List<IFormFile> Files { get; set; }
        public List<string> Tos { get; set; }
        public List<string> Ccs { get; set; }
        public List<string> Bccs { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}

using Application;
using Application.BaseCommand;
using Application.Common.Interfaces.Application;
using Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace NNanh.Zolo.Controllers
{
    /// <summary>
    /// Base nghiệp vụ cơ bản thêm sửa xóa
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class BaseBussinessController<TEntity> : ApiControllerBase
    {
        private IAppService _appService;

        protected IAppService AppService => _appService ??= HttpContext.RequestServices.GetService<IAppService>();

        [HttpGet]
        public async Task<ActionResult<PaginatedList<TEntity>>> GetWithPagination([FromQuery] BaseQueryCommand<TEntity> query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseResultModel>> Create(CreateBaseCommand<TEntity> command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut]
        public async Task<ActionResult<ResponseResultModel>> Update(UpdateBaseCommand<TEntity> command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete]
        public async Task<ActionResult<ResponseResultModel>> Delete(DeleteBaseCommand<TEntity> command)
        {
            return await Mediator.Send(command);
        }
    }
}

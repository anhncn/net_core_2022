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
    [Microsoft.AspNetCore.Authorization.Authorize]
    public abstract class BaseBussinessController<TEntity> : ApiControllerBase
    {
        private IAppService _appService;

        protected IAppService AppService => _appService ??= HttpContext.RequestServices.GetService<IAppService>();

        [HttpGet]
        public virtual async Task<ActionResult<PaginatedList<TEntity>>> GetWithPagination([FromQuery] BaseQueryCommand<TEntity> query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public virtual async Task<ActionResult<ResponseResultModel>> Create(TEntity entity)
        {
            return await Mediator.Send(CreateBaseCommand<TEntity>.Instance(entity));
        }

        [HttpPut]
        public virtual async Task<ActionResult<ResponseResultModel>> Update(TEntity entity)
        {
            return await Mediator.Send(UpdateBaseCommand<TEntity>.Instance(entity));
        }

        [HttpDelete]
        public virtual async Task<ActionResult<ResponseResultModel>> Delete(string id)
        {
            return await Mediator.Send(DeleteBaseCommand<TEntity>.Instance(id));
        }
    }
}

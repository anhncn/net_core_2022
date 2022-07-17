using Application;
using Application.MediatorHandler.Grade.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace NNanh.Zolo.Controllers.BaseDefine
{
    [Route("api/grades")]
    public class GradesController : BaseBussinessController<Domain.Entities.Grade>
    {
        [HttpGet]
        public override async Task<ActionResult<PaginatedList<Domain.Entities.Grade>>> GetWithPagination([FromQuery] BaseQueryCommand<Domain.Entities.Grade> query)
        {
            return await Mediator.Send(new GradeQueriesCommand() { PageIndex = query.PageIndex, PageSize = query.PageIndex });
        }
    }
}

using Application;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace NNanh.Zolo.Controllers.BaseDefine
{
    [Route("api/grades")]
    public class GradesController : BaseBussinessController<Domain.Entities.Grade> { }
}

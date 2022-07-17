using Microsoft.AspNetCore.Mvc;

namespace NNanh.Zolo.Controllers.BaseDefine
{
    [Route("api/school-years")]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class SchoolYearsController : BaseBussinessController<Domain.Entities.SchoolYear>
    {
    }
}

using Microsoft.AspNetCore.Mvc;

namespace NNanh.Zolo.Controllers
{
    [Route("api/roles")]
    public class RolesController : BaseBussinessController<Domain.Entities.RoleDefine>
    {
    }
}

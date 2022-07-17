using Microsoft.AspNetCore.Mvc;

namespace NNanh.Zolo.Controllers.BaseDefine
{
    [Route("api/organizations")]
    public class OrganizationsController : BaseBussinessController<Domain.Entities.Organization>
    {
    }
}

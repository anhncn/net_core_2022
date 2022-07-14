using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace NNanh.Zolo.Controllers
{
    //[Authorize]
    [Route("api/users")]
    public class UsersController : BaseBussinessController<UserSql>
    {
        public UsersController()
        {
            AppService.LogService.Info("NLog injected into UsersController");
        }

        [HttpGet("set-cache")]
        public async Task<IActionResult> SetCacheName()
        {
            await AppService.CacheService.SetAsync("UserName", 60, "Phương");
            return Ok();
        }

        [HttpGet("get-cache")]
        public async Task<IActionResult> GetCacheName()
        {
            var userName = await AppService.CacheService.GetAsync<string>("UserName");
            return Ok(userName);
        }
    }
}

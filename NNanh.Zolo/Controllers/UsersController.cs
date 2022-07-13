﻿using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace NNanh.Zolo.Controllers
{
    //[Authorize]
    public class UsersController : BaseBussinessController<UserSql>
    {
        public UsersController()
        {
            AppService.LogService.Info("NLog injected into UsersController");
        }

        [Route("set-cache")]
        public async Task<IActionResult> SetCacheName()
        {
            await AppService.CacheService.SetAsync("UserName", 60, "Phương");
            return Ok();
        }

        [Route("get-cache")]
        public async Task<IActionResult> GetCacheName()
        {
            var userName = await AppService.CacheService.GetAsync<string>("UserName");
            return Ok(userName);
        }

        [Route("user-name")]
        public IActionResult GetUserName()
        {
            return Ok(AppService.UserService.UserName);
        }

    }
}

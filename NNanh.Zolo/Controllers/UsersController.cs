
using Application;
using Application.BaseCommand;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System;
using Domain.Common;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Application;

namespace NNanh.Zolo.Controllers
{
    //[Authorize]
    public class UsersController : ApiControllerBase
    {
        private readonly ILogService _logger;
        private readonly ICacheService _cache;
        private readonly ApplicationSetting _appSetting;
        private readonly IUserService _userService;

        public UsersController(IAppService appService)
        {
            _appSetting = appService.ApplicationSetting;
            _userService = appService.UserService;
            _cache = appService.CacheService;
            _logger = appService.LogService;
            _logger.Info("NLog injected into UsersController");
        }


        [HttpGet]
        public async Task<ActionResult<PaginatedList<UserSql>>> GetWithPagination([FromQuery] BaseQueryCommand<UserSql> query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseResult>> Create(CreateBaseCommand<UserSql> command)
        {
            return await Mediator.Send(command);
        }

        [Route("set-cache")]
        public async Task<IActionResult> SetCacheName()
        {
            await _cache.SetAsync("UserName", 60, "Phương");
            return Ok();
        }

        [Route("get-cache")]
        public async Task<IActionResult> GetCacheName()
        {
            var userName = await _cache.GetAsync<string>("UserName");
            return Ok(userName);
        }

        [Route("user-name")]
        public IActionResult GetUserName()
        {
            return Ok(_userService.UserName);
        }

    }
}

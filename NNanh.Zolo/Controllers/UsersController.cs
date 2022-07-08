
using Application;
using Application.BaseCommand;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System;
using Microsoft.Extensions.Options;
using Domain.Common;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Application;

namespace NNanh.Zolo.Controllers
{
    [Authorize]
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
        public async Task<ActionResult<PaginatedList<UserInt>>> GetWithPagination([FromQuery] BaseQueryCommand<UserInt> query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Create(CreateBaseCommand<UserInt> command)
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

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate()
        {
            var token = AuthenticatePrivate();

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }

        private JwtTokens AuthenticatePrivate()
        {
            // Else we generate JSON Web Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_appSetting.Jwt.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, "Ngọc Anh")
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new JwtTokens { Token = tokenHandler.WriteToken(token) };

        }
    }
}

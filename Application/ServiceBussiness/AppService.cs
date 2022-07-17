using Application.Common.Interfaces;
using Application.Common.Interfaces.Application;
using Application.Common.Interfaces.Services;
using Application.Common.Interfaces.WebUI;
using Domain.Common;
using Microsoft.Extensions.Options;

namespace Application.ServiceBussiness
{
    public class AppService : IAppService
    {
        private readonly ILogService _log;
        private readonly ICacheService _cache;
        private readonly ApplicationSetting _appSetting;
        private readonly IUserService _userService;
        private readonly IIDentityService _iDentityService;

        public ILogService LogService => _log;
        public ICacheService CacheService => _cache;
        public ApplicationSetting ApplicationSetting => _appSetting;
        public IUserService UserService => _userService;
        public IIDentityService IDentityService => _iDentityService;

        public AppService(ILogService logger, ICacheService cache,
            IIDentityService iDentityService,
            IOptions<ApplicationSetting> options, IUserService userService)
        {
            _log = logger;
            _cache = cache;
            _appSetting = options.Value;
            _userService = userService;
            _iDentityService = iDentityService;
        }
    }
}

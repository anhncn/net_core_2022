using Application.Common.Interfaces;
using Application.Common.Interfaces.Application;
using Application.Common.Interfaces.Services;
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

        public ILogService LogService => _log;
        public ICacheService CacheService => _cache;
        public ApplicationSetting ApplicationSetting => _appSetting;
        public IUserService UserService => _userService;

        public AppService(ILogService logger, ICacheService cache,
            IOptions<ApplicationSetting> options, IUserService userService)
        {
            _log = logger;
            _cache = cache;
            _appSetting = options.Value;
            _userService = userService;
        }
    }
}

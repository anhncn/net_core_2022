using Application.Common.Interfaces.Services;
using Application.Common.Interfaces.WebUI;
using Domain.Common;

namespace Application.Common.Interfaces.Application
{
    public interface IAppService
    {
        public ILogService LogService { get; }
        public ICacheService CacheService { get; }
        public ApplicationSetting ApplicationSetting { get; }
        public IUserService UserService { get; }
        public IIDentityService IDentityService { get; }
    }
}

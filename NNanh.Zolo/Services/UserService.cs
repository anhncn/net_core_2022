using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NNanh.Zolo.Services
{
    public class UserService : IUserService
    {

        private readonly IHttpContextAccessor _httpContext;

        public UserService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public Task<string> GetUserId()
        {
            if(_httpContext.HttpContext != null)
            {
                return Task.FromResult(_httpContext.HttpContext.User.FindFirstValue(ClaimTypes.Name));
            }
            return Task.FromResult(string.Empty);
        }

        public Task<string> GetUserName()
        {
            if (_httpContext.HttpContext != null)
            {
                return Task.FromResult(_httpContext.HttpContext.User.FindFirstValue(ClaimTypes.Name));
            }
            return Task.FromResult(string.Empty);
        }
    }
}

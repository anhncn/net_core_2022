using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NNanh.Zolo.Services
{
    public class UserService : IUserService
    {

        private readonly IHttpContextAccessor _httpContext;

        public string UserId
        {
            get
            {
                if (_httpContext.HttpContext != null)
                {
                    return _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                }
                return string.Empty;
            }
        }

        public string UserName
        {
            get
            {
                if (_httpContext.HttpContext != null)
                {
                    return _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.Name);
                }
                return string.Empty;
            }
        }

        public UserService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

    }
}

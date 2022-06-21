using Application.Common.Interfaces.Services;
using Application.Service.Interface;
using Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Service
{
    //public class LoginContextService : DbService<User>, ILoginService
    //{
    //    public LoginContextService(IApplicationDbContext context) : base(context)
    //    {

    //    }

    //    public Task<bool> LoginAsync(User user)
    //    {
    //        var findUser = Context.Set<User>()
    //            .AsQueryable()
    //            .FirstOrDefault(x => x.UserName == user.UserName && x.PassWord == user.PassWord);

    //        if (findUser == null)
    //        {
    //            return Task.FromResult(false);
    //        }
    //        return Task.FromResult(true);
    //    }
    //}
}

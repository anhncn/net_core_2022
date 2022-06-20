using Application.Common.Interfaces.Services;
using Application.Service.Interface;
using Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Service
{
    //public class LoginContextService : DbContextService<User>, ILoginContextService
    //{
    //    public LoginContextService(IAppDbContext<User> context) : base(context)
    //    {

    //    }

    //    public Task<bool> LoginAsync(User user)
    //    {
    //        var findUser = _context.GetAll().FirstOrDefault(x => x.UserName == user.UserName && x.PassWord == user.PassWord);
    //        if (findUser == null)
    //        {
    //            return Task.FromResult(false);
    //        }
    //        return Task.FromResult(true);
    //    }
    //}
}

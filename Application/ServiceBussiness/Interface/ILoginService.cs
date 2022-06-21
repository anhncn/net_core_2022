using Application.Common.Interfaces.Services;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Service.Interface
{
    public interface ILoginService : IDbService<User>
    {
        Task<bool> LoginAsync(User user);
    }
}

using Application.Common.Interfaces.Services;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Service.Interface
{
    public interface ILoginContextService : IDbContextService<UserInt>
    {
        Task<bool> LoginAsync(UserInt user);
    }
}

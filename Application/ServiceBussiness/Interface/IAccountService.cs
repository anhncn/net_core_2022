using Application.BaseCommand;
using Application.Common.Interfaces.Services;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Service.Interface
{
    public interface IAccountService : IDbService<Account>
    {
        Task<ResponseResult> LoginAsync(Account account);
        Task<ResponseResult> RegisterAsync(Account account);
    }
}

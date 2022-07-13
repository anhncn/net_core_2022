using Application.BaseCommand;
using Application.Common.Interfaces.Services;
using Application.Common.Interfaces.WebUI;
using Application.Service;
using Application.Service.Interface;
using Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.ServiceBussiness.Implement
{
    public class AccountContextService : DbService<Account>, IAccountService
    {

        private readonly ITokenAuthService _tokenService;

        public AccountContextService(IApplicationDbContext context, ITokenAuthService tokenService) : base(context)
        {
            _tokenService = tokenService;
        }
        #region Login
        public async Task<ResponseResult> LoginAsync(Account account)
        {
            if (!await CheckExistAccount(account))
            {
                throw new Exception("Wrong UserName or Password!");
            }

            return ResponseResult.Instance(_tokenService.Generate(account.UserName));
        }

        private Task<bool> CheckExistAccount(Account account)
        {
            var hashPassword = _tokenService.HashPassword(account.Password);
            var findUser = Context.Set<Account>().AsQueryable()
                .FirstOrDefault(rec => rec.UserName == account.UserName && rec.Password == hashPassword);

            if (findUser == null) return Task.FromResult(false);

            return Task.FromResult(true);
        }

        #endregion

        #region Register

        public async Task<ResponseResult> RegisterAsync(Account account)
        {
            if (account == null) throw new ArgumentNullException(nameof(account));
            if (string.IsNullOrEmpty(account.UserName)) throw new ArgumentNullException(nameof(account.UserName));
            if (string.IsNullOrEmpty(account.Password)) throw new ArgumentNullException(nameof(account.Password));

            if (await CheckExistUserName(account.UserName))
            {
                throw new Exception($"UserName {account.UserName} is existed!");
            }

            account.Password = _tokenService.HashPassword(account.Password);

            await AddAsync(account);

            var result = await Context.SaveChangesAsync(new System.Threading.CancellationToken());

            return ResponseResult.Instance(result);
        }

        private Task<bool> CheckExistUserName(string userName)
        {
            var findUser = Context.Set<Account>().AsQueryable()
                .FirstOrDefault(rec => rec.UserName == userName);

            if (findUser == null) return Task.FromResult(false);

            return Task.FromResult(true);
        }

        #endregion
    }
}

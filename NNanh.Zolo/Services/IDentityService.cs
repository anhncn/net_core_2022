using Application.Common.Interfaces;
using Application.Common.Interfaces.Services;
using Application.Common.Interfaces.WebUI;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NNanh.Zolo.Services
{
    public class IDentityService : IIDentityService
    {

        private readonly IUserService _userService;
        private readonly IDbService _dbService;

        public IDentityService(IUserService userService, IDbService dbService)
        {
            _userService = userService;
            _dbService = dbService;
        }

        public Task<IEnumerable<string>> GetRoles()
        {
            Guid.TryParse(_userService.UserId, out Guid userId);
            return GetRolesByUserId(userId);
        }

        public Task<IEnumerable<string>> GetRolesByUserId(Guid userId)
        {
            var roles = _dbService.AsQueryable<AccountRole>()
                .Where(item => item.AccountId == userId)
                .Select(o => o.RoleDefineCode).AsEnumerable();

            return Task.FromResult(roles);
        }

        public Task<Guid> GetSchoolYearId()
        {
            Guid.TryParse(_userService.UserId, out Guid userId);

            var accRole = _dbService.AsQueryable<AccountRole>()
                .FirstOrDefault(item => item.AccountId == userId);

            return Task.FromResult(accRole == null ? Guid.Empty : accRole.SchoolYearId);
        }
    }
}

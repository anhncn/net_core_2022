using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.WebUI
{
    public interface IIDentityService 
    {
        Task<IEnumerable<string>> GetRoles();

        Task<Guid> GetSchoolYearId();
    }
}

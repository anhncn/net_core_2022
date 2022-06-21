using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IUserService
    {
        Task<string> GetUserId();
        
        Task<string> GetUserName();
    }
}

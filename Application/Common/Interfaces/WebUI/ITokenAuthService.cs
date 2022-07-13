using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Interfaces.WebUI
{
    public interface ITokenAuthService
    {
        JwtTokens Generate(string userName);

        bool ComparePassword(string rawData, string hashString);

        string HashPassword(string rawData);
    }
}

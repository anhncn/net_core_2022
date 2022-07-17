using Domain.Common;

namespace Application.Common.Interfaces.WebUI
{
    public interface ITokenAuthService
    {
        JwtTokens Generate(Domain.Entities.Account account);

        bool ComparePassword(string rawData, string hashString);

        string HashPassword(string rawData);
    }
}

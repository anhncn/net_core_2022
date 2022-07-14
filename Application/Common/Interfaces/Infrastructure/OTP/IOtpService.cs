using Domain.Common.OTP;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Infrastructure
{
    public interface IOtpService
    {
        Task SendOTP(string key, OTPHelper helper);

        Task<OTPHelper> GetOTP(string key);

        Task<bool> CheckOTP(string key, string value);

    }
}

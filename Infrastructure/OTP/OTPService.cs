using Application.Common.Interfaces.Application;
using Application.Common.Interfaces.Infrastructure;
using Domain.Common.OTP;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.OTP
{
    public class OTPService : IOtpService
    {

        private const double TIME_EXPIRE_OTP = 5;
        private const int LENGTH_OTP_CODE = 6;

        private readonly IAppService _appService;
        public OTPService(IAppService appService)
        {
            _appService = appService;
        }

        public async Task<bool> CheckOTP(string key, string value)
        {
            var otpCache = await GetOTP(key);

            if (otpCache == null || otpCache.OTPValue != value) return false;

            return true;
        }

        public Task<OTPHelper> GetOTP(string key)
        {
            return _appService.CacheService.GetAsync<OTPHelper>(key);
        }

        public async Task SendOTP(string key, OTPHelper helper)
        {
            helper.OTPValue = GenerateOTP();
            helper.TimeSend = DateTime.Now;
            await _appService.CacheService.SetAsync(key, TIME_EXPIRE_OTP, helper);

            switch (helper.Type)
            {
                case Domain.Enumerations.OTPSendType.Logging:
                    _appService.LogService.Info($"Xác nhận mã OTP gửi KH {key}: {helper.OTPValue}");
                    break;
                case Domain.Enumerations.OTPSendType.SMS:
                case Domain.Enumerations.OTPSendType.Email:
                default:
                    throw new NotImplementedException();
            }
        }

        private string GenerateOTP()
        {
            Random random = new Random();
            StringBuilder builder = new StringBuilder();
            builder.Clear();

            for (int i = 0; i <= LENGTH_OTP_CODE; i++)
            {
                builder.Append(random.Next(0, 9));
            }

            return builder.ToString();
        }
    }
}

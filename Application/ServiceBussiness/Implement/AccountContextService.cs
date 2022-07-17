using Domain.Model;
using Application.Common.Interfaces.Application;
using Application.Common.Interfaces.Infrastructure;
using Application.Common.Interfaces.Services;
using Application.Common.Interfaces.WebUI;
using Application.MediatorHandler.Account.Commands;
using Application.MediatorHandler.Account.Commands.ForgotPassword;
using Application.Service.Interface;
using Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.ServiceBussiness.Implement
{
    public class AccountContextService : IAccountService
    {
        private const int MIN_SECONDS_TIME_RESEND_OTP = 60;

        protected readonly IDbService DbService;

        private readonly ITokenAuthService _tokenService;
        private readonly IOtpService _otpService;
#pragma warning disable IDE0052 // Remove unread private members
        private readonly IAppService _appService;
#pragma warning restore IDE0052 // Remove unread private members

        public AccountContextService(
            IDbService dbService,
            ITokenAuthService tokenService,
            IAppService appService,
            IOtpService otpService
            )
        {
            DbService = dbService;
            _appService = appService;
            _tokenService = tokenService;
            _otpService = otpService;
        }
        #region Login

        public async Task<ResponseResultModel> LoginAsync(LoginAccountCommand account)
        {
            var accountResource = await FindAccount(account.UserName, account.Password);

            if (accountResource == null)
            {
                throw new Exception("Wrong UserName or Password!");
            }

            return ResponseResultModel.Instance(_tokenService.Generate(accountResource));
        }

        private Task<Account> FindAccount(string userName, string password)
        {
            var hashPassword = _tokenService.HashPassword(password);
            var findUser = DbService.AsQueryable<Account>()
                .FirstOrDefault(rec => rec.UserName == userName && rec.Password == hashPassword);

            return Task.FromResult(findUser);
        }

        #endregion

        #region Register

        public async Task<ResponseResultModel> RegisterAsync(RegisterAccountCommand account)
        {
            if (account == null) throw new ArgumentNullException(nameof(account));
            if (string.IsNullOrEmpty(account.UserName)) throw new ArgumentNullException(nameof(account.UserName));
            if (string.IsNullOrEmpty(account.Password)) throw new ArgumentNullException(nameof(account.Password));

            if (await ExistUserName(account.UserName)) throw new Exception($"UserName {account.UserName} is existed!");

            var entity = new Account()
            {
                UserName = account.UserName,
                Password = _tokenService.HashPassword(account.Password),
                FirstName = account.FirstName,
                LastName = account.LastName
            };

            await DbService.AddAsync(entity);

            var result = await DbService.Context.SaveChangesAsync(new System.Threading.CancellationToken());

            return ResponseResultModel.Instance(result);
        }

        private Task<bool> ExistUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName)) return Task.FromResult(false);

            var findUser = DbService.AsQueryable<Account>()
                .FirstOrDefault(rec => rec.UserName == userName);

            if (findUser == null) return Task.FromResult(false);

            return Task.FromResult(true);
        }

        #endregion

        #region ForgotPassword

        public async Task<ResponseResultModel> ForgotPassword(ForgotPasswordAccountCommand account)
        {
            if (!await ExistUserName(account.UserName)) throw new Exception($"UserName {account.UserName} isn't existed!");

            await _otpService.SendOTP(GetKeyOTPForgotPassword(account.UserName), new Domain.Common.OTP.OTPHelper() { Type = Domain.Enumerations.OTPSendType.Logging });

            return ResponseResultModel.Instance($"Đã gửi mã OTP cho KH {account.UserName}, kiểm tra hộp thư hay điện thoại của bạn!");
        }

        public async Task<ResponseResultModel> ForgotPasswordConfirm(ForgotPasswordConfirmAccountCommand account)
        {
            if (!await _otpService.CheckOTP(GetKeyOTPForgotPassword(account.UserName), account.OTPValue))
            {
                throw new Exception("Mã OTP không chính xác!");
            }

            return ResponseResultModel.Instance(new { Success = true, Message = $"Kiểm tra mã OTP Thành công!" });
        }

        public async Task<ResponseResultModel> ForgotPasswordOtpResend(ForgotPasswordOtpResendAccountCommand account)
        {
            var helperSource = await _otpService.GetOTP(GetKeyOTPForgotPassword(account.UserName));

            if (helperSource != null)
            {
                if (helperSource.TimeSend != null && DateTime.Now.Subtract(helperSource.TimeSend).TotalSeconds <= MIN_SECONDS_TIME_RESEND_OTP)
                {
                    throw new Exception($"Sau {MIN_SECONDS_TIME_RESEND_OTP}s gửi lại OTP!");
                }

                helperSource.CountSend += 1;
            }

            helperSource.Type = Domain.Enumerations.OTPSendType.Logging;

            await _otpService.SendOTP(GetKeyOTPForgotPassword(account.UserName), helperSource);

            return ResponseResultModel.Instance($"Đã gửi mã OTP cho KH {account.UserName}, kiểm tra hộp thư hay điện thoại của bạn!");
        }

        public async Task<ResponseResultModel> ForgotPasswordChangePasswordAfterCofirmSuccess(ForgotPasswordChangePasswordAfterCofirmSuccessAccountCommand accountModel)
        {
            // Đoạn này tạm thời tận dụng lại OTP đã sinh từ lúc đầu, sai về nguyên tắc nhưng dùng tạm đã
            // Vì nếu dùng lại thì mã này sẽ hết hạn sớm gây nên việc user chưa kịp đổi pass đã expire-otp
            if (!await _otpService.CheckOTP(GetKeyOTPForgotPassword(accountModel.UserName), accountModel.OTPValue))
            {
                throw new Exception("Có lỗi xảy ra!");
            }

            if (!CheckPasswordRegex(accountModel.PasswordNew)) throw new Exception("Password not regex!");

            if (accountModel.PasswordNew != accountModel.PasswordConfirm) throw new Exception("PasswordConfirm not match!");

            var account = DbService.AsQueryable<Account>().FirstOrDefault(a => a.UserName == accountModel.UserName);

            account.Password = _tokenService.HashPassword(accountModel.PasswordNew);

            await DbService.UpdateAsync(account);

            await DbService.Context.SaveChangesAsync(new System.Threading.CancellationToken());

            return ResponseResultModel.Instance($"Đổi mật khẩu thành công");
        }

        private string GetKeyOTPForgotPassword(string userName) => "ForgotPass_" + userName;

        private bool CheckPasswordRegex(string password)
        {
            if (string.IsNullOrEmpty(password)) return false;

            if (password.Length < 8) return false;

            return true;
        }

        #endregion
    }
}

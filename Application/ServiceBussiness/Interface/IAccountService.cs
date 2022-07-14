using Application.BaseCommand;
using Application.Common.Interfaces.Services;
using Application.MediatorHandler.Account.Commands;
using Application.MediatorHandler.Account.Commands.ForgotPassword;
using Domain.Entities;
using Domain.Model;
using System.Threading.Tasks;

namespace Application.Service.Interface
{
    public interface IAccountService : IDbService<Account>
    {
        /// <summary>
        /// Kiểm tra tồn tại tài khoản không
        /// Nếu tồn tại thì trả về token
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        Task<ResponseResultModel> LoginAsync(LoginAccountCommand account);

        /// <summary>
        /// Kiểm tra thông tin truyền vào có rỗng hoặc null thì văng lỗi
        /// Kiểm tra có userName này chưa có rồi thì văng lỗi
        /// Pass hết thì băm mật khẩu và lưu thông tin tài khoản mới
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        Task<ResponseResultModel> RegisterAsync(RegisterAccountCommand account);

        /// <summary>
        /// kiểm tra tài khoản tồn tại ko
        /// sinh mã OTP gửi cho KH qua email, sđt, logging
        /// lưu mã OTP vào cache để kiểm tra
        /// trả về result
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        Task<ResponseResultModel> ForgotPassword(ForgotPasswordAccountCommand account);

        /// <summary>
        /// Kiểm tra userName
        /// Kiểm tra mã OTP
        /// Kiểm tra mã OTP và userName có trùng với trong cache không
        /// Thành công trả về 1 mã sinh ngẫu nhiên lưu vào cache để validate bước đổi mật khẩu
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        Task<ResponseResultModel> ForgotPasswordConfirm(ForgotPasswordConfirmAccountCommand account);

        /// <summary>
        /// kiểm tra tài khoản tồn tại không
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        Task<ResponseResultModel> ForgotPasswordOtpResend(ForgotPasswordOtpResendAccountCommand account);

        /// <summary>
        /// Kiểm tra mã sinh ngẫu nhiên
        /// Kiểm tra mật khẩu đổi mới theo rule
        /// Kiểm tra xác nhận mật khẩu có trùng nhau ko
        /// Băm mật khẩu mới rồi cập nhật vào DB 
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        Task<ResponseResultModel> ForgotPasswordChangePasswordAfterCofirmSuccess(ForgotPasswordChangePasswordAfterCofirmSuccessAccountCommand account);
    }
}

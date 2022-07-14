using Application.BaseCommand;
using Application.MediatorHandler.Account.Commands;
using Application.MediatorHandler.Account.Commands.ForgotPassword;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace NNanh.Zolo.Controllers
{
    public class AccountsController : ApiControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<ResponseResult>> Login(LoginAccountCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("register")]
        public async Task<ActionResult<ResponseResult>> Register(RegisterAccountCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("forgot-password")]
        public async Task<ActionResult<ResponseResult>> ForgotPassword(ForgotPasswordAccountCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("forgot-password/otp/resend")]
        public async Task<ActionResult<ResponseResult>> ForgotPasswordOtpResend(ForgotPasswordOtpResendAccountCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("forgot-password/confirm")]
        public async Task<ActionResult<ResponseResult>> ForgotPasswordConfirm(ForgotPasswordConfirmAccountCommand command)
        {
            return await Mediator.Send(command);
        }
        [HttpPost("forgot-password/confirm/change-password")]
        public async Task<ActionResult<ResponseResult>> ForgotPasswordChangePasswordAfterConfirmSuccess(ForgotPasswordChangePasswordAfterCofirmSuccessAccountCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}

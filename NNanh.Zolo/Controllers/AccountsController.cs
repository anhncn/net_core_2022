using Domain.Model;
using Application.MediatorHandler.Account.Commands;
using Application.MediatorHandler.Account.Commands.ForgotPassword;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace NNanh.Zolo.Controllers
{
    [Route("api/accounts")]
    public class AccountsController : ApiControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<ResponseResultModel>> Login(LoginAccountCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("register")]
        public async Task<ActionResult<ResponseResultModel>> Register(RegisterAccountCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("forgot-password")]
        public async Task<ActionResult<ResponseResultModel>> ForgotPassword(ForgotPasswordAccountCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("forgot-password/otp/resend")]
        public async Task<ActionResult<ResponseResultModel>> ForgotPasswordOtpResend(ForgotPasswordOtpResendAccountCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("forgot-password/confirm")]
        public async Task<ActionResult<ResponseResultModel>> ForgotPasswordConfirm(ForgotPasswordConfirmAccountCommand command)
        {
            return await Mediator.Send(command);
        }
        [HttpPost("forgot-password/confirm/change-password")]
        public async Task<ActionResult<ResponseResultModel>> ForgotPasswordChangePasswordAfterConfirmSuccess(ForgotPasswordChangePasswordAfterCofirmSuccessAccountCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}

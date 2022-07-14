using Domain.Entities;

namespace Domain.Model
{
    public class AccountModel : Account
    {
        public string OTPValue { get; set; }

        public string PasswordNew { get; set; }
        public string PasswordConfirm { get; set; }
    }
}

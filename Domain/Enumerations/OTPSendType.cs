using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enumerations
{
    public enum OTPSendType : int
    {
        Logging = 0,
        SMS = 1,
        Email = 2,
    }
}

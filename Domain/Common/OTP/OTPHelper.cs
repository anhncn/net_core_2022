using Domain.Enumerations;
using System;

namespace Domain.Common.OTP
{
    public class OTPHelper
    {
        /// <summary>
        /// Kiểu gửi mã OTP
        /// </summary>
        public OTPSendType Type { get; set; }

        /// <summary>
        /// Địa chỉ gửi mã OTP đến
        /// </summary>
        public string Desination { get; set; }

        /// <summary>
        /// Giá trị của mã OTP
        /// </summary>
        public string OTPValue { get; set; }

        /// <summary>
        /// Thời gian gửi OTP lần trước
        /// </summary>
        public DateTime TimeSend { get; set; }

        /// <summary>
        /// Số lần gửi
        /// </summary>
        public int CountSend { get; set; }
    }
}

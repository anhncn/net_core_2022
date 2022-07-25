using System;

namespace Application.Common.Interfaces.Services
{
    /// <summary>
    /// Log 
    /// </summary>
    public interface ILogService
    {
        void Info(string message);

        void Error(Exception exception, string message = "");

    }
}

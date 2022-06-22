using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Interfaces.Services
{
    /// <summary>
    /// Log 
    /// </summary>
    public interface ILogService
    {
        void Info(string message);

        void Error(string message);

        void Error(Exception exception);
    }
}

using Application.Common.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System;

namespace Infrastructure.Logs
{
    public class NLogService : ILogService
    {
        private readonly ILogger<NLogService> _logger;

        public NLogService(ILogger<NLogService> logger)
        {
            _logger = logger;
        }

        public void Error(Exception exception, string message = "")
        {
            _logger.LogError(0, exception, message ?? exception.Message);
        }

        public void Info(string message)
        {
            _logger.LogInformation(message);
        }
    }
}

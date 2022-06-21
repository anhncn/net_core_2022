using Application.Common.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System;

namespace Infrastructure.Logs
{
    public class NLogService : ILogService
    {
        private readonly ILogger<NLogService> logger;

        public NLogService(ILogger<NLogService> logger)
        {
            this.logger = logger;
        }

        public void Error(string message)
        {
            logger.LogError(message);
        }

        public void Error(Exception exception)
        {
            logger.LogError(1, exception, message: "");
        }

        public void Info(string message)
        {
            logger.LogInformation(message);
        }
    }
}

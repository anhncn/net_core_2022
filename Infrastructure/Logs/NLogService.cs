using Application.Common.Interfaces;
using Application.Common.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Infrastructure.Logs
{
    public class NLogService : ILogService
    {
        private readonly IUserService _userService;
        private readonly ILoggerFactory _factory;

        public NLogService(IUserService userService, ILoggerFactory factory)
        {
            _userService = userService;
            _factory = factory;
        }

        private IEnumerable<KeyValuePair<string, object>> GetProperties()
        {
            return new[] { 
                new KeyValuePair<string, object>(nameof(_userService.UserId), _userService.UserId),
                new KeyValuePair<string, object>(nameof(_userService.UserName), _userService.UserName),
            };
        }

        private void Log(LogLevel logLevel, Exception exception, string message = "")
        {
            _factory.CreateLogger("elk").Log(logLevel, eventId: 0, GetProperties(), exception, (l, e) => message);
        }

        public void Error(Exception exception, string message = "")
        {
            if (string.IsNullOrEmpty(message))
            {
                message = exception.Message;
            }
            Log(LogLevel.Error, exception, message);
        }

        public void Info(string message)
        {
            Log(LogLevel.Information, null, message);
        }
    }
}

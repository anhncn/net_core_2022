using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Behaviours
{
    /// <summary>
    /// Tiền xử lý sẽ ghi log
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
    {
        public LoggingBehaviour()
        {
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            //var requestName = typeof(TRequest).Name;

            return Task.FromResult("");
            //var userId = _currentUserService.UserId ?? string.Empty;
            //string userName = string.Empty;

            //if (!string.IsNullOrEmpty(userId))
            //{
            //    userName = await _identityService.GetUserNameAsync(userId);
            //}

            //_logger.LogInformation("CleanArchitecture Request: {Name} {@UserId} {@UserName} {@Request}",
            //    requestName, userId, userName, request);
        }
    }
}

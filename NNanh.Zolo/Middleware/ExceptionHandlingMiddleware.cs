using Application.Common.Exceptions;
using Application.Common.Interfaces.Services;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace NNanh.Zolo.MiddleWare
{
    internal sealed class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogService _logger;

        public ExceptionHandlingMiddleware(ILogService logger) => _logger = logger;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                _logger.Error(e);

                await HandleExceptionAsync(context, e);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var statusCode = GetStatusCode(exception);

            var response = new
            {
                title = GetTitle(exception),
                status = statusCode,
                detail = exception.Message,
                errors = GetErrors(exception)
            };

            httpContext.Response.ContentType = "application/json";

            httpContext.Response.StatusCode = statusCode;

            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        private static int GetStatusCode(Exception exception)
        {
            if (exception.GetType().Equals(typeof(ValidationException)))
            {
                return StatusCodes.Status422UnprocessableEntity;
            }
            else if (exception.GetType().Equals(typeof(ForbiddenAccessException)))
            {
                return StatusCodes.Status403Forbidden;
            }
            else
            {
                return StatusCodes.Status500InternalServerError;
            }
        }

        private static string GetTitle(Exception exception) =>
            exception switch
            {
                _ => "Server Error"
            };

        private static IDictionary<string, string[]> GetErrors(Exception exception)
        {
            IDictionary<string, string[]> errors = null;

            if (exception is ValidationException validationException)
            {
                errors = validationException.Errors;
            }

            return errors;
        }
    }
}

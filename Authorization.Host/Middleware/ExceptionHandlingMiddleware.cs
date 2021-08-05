using System;
using System.Threading.Tasks;
using Authorization.Application.Extensions;
using Authorization.Application.Models.Responses;
using Authorization.Infrastructure.Logging.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using ApplicationException = Authorization.Domain.Exceptions.ApplicationException;

namespace Authorization.Host.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private static readonly ILogger Logger = Log.ForContext<ExceptionHandlingMiddleware>();
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ApplicationException e)
            {
                var errorCodeDisplayName = e.ErrorCode.GetDisplayName();
                Logger.Error("ApplicationException: {@ErrorCode}", errorCodeDisplayName);

                if (context.Response.HasStarted)
                {
                    throw;
                }

                context.Response.Clear();

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int) e.StatusCode;

                var errorResponse = new ErrorResponse
                {
                    Code = errorCodeDisplayName,
                    Message = e.Message,
                };

                var error = JsonConvert.SerializeObject(errorResponse);

                await context.Response.WriteAsync(error);
            }
            catch (Exception e)
            {
                var exceptionInfo = ExceptionInfo.Create(e);
                Logger.Error("Exception: {@ExceptionInfo}", exceptionInfo);

                if (context.Response.HasStarted)
                {
                    throw;
                }

                context.Response.Clear();

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var error = JsonConvert.SerializeObject(exceptionInfo);

                await context.Response.WriteAsync(error);
            }
        }
    }
}

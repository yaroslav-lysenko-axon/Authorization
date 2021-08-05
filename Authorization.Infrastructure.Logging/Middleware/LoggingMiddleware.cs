using System.Diagnostics;
using System.Threading.Tasks;
using Authorization.Infrastructure.Logging.LogConfiguration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Features;
using Serilog;
using Serilog.Context;

namespace Authorization.Infrastructure.Logging.Middleware
{
    public class LoggingMiddleware
    {
        private const string MessageTemplate = "[{RemoteIp}:{RemotePort}] {RequestMethod} {Route} responded {StatusCode} in {Elapsed:0.0000} ms";

        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var requestUri = context.Request.GetDisplayUrl();

            var connection = context.Features.Get<IHttpConnectionFeature>();

            using (LogContext.PushProperty(LogConstants.ServerIpAddress, connection.LocalIpAddress))
            using (LogContext.PushProperty(LogConstants.ClientIpAddress, connection.RemoteIpAddress))
            using (LogContext.PushProperty(LogConstants.RequestMethod, context.Request.Method))
            using (LogContext.PushProperty(LogConstants.RequestDisplayUrl, requestUri))
            using (LogContext.PushProperty(LogConstants.Host, context.Request.Host))
            {
                var start = Stopwatch.GetTimestamp();

                await _next.Invoke(context);

                var elapsedMs = GetElapsedMilliseconds(start, Stopwatch.GetTimestamp());

                Log.Information(
                    MessageTemplate,
                    connection.RemoteIpAddress,
                    connection.RemotePort,
                    context.Request.Method,
                    requestUri,
                    context.Response.StatusCode,
                    elapsedMs);
            }
        }

        private static double GetElapsedMilliseconds(long start, long stop)
        {
            return (stop - start) * 1000 / (double)Stopwatch.Frequency;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Authorization.Infrastructure.Logging.Models
{
    [Serializable]
    public class ExceptionInfo
    {
        private const string MsStackTraceKey = "MS_LoggedBy";

        private ExceptionInfo() { }

        [JsonProperty("type")]
        public string Type { get; private set; }

        [JsonProperty("message")]
        public string Message { get; private set; }

        [JsonProperty("stackTrace")]
        public string StackTrace { get; private set; }

        [JsonProperty("innerExceptionInfo", NullValueHandling = NullValueHandling.Ignore)]
        public ExceptionInfo InnerExceptionInfo { get; private set; }

        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public IReadOnlyDictionary<object, object> Data { get; private set; }

        public static ExceptionInfo Create(Exception exception)
        {
            var info = new ExceptionInfo();

            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            exception.Demystify();

            info.Type = exception.GetType().FullName;
            info.Message = exception.Message;
            info.StackTrace = exception.StackTrace;
            info.Data = SerializeData(exception);

            if (exception.InnerException != null)
            {
                info.InnerExceptionInfo = Create(exception.InnerException);
            }

            return info;
        }

        private static IReadOnlyDictionary<object, object> SerializeData(Exception exception)
        {
            var data = new Dictionary<object, object>(exception.Data.Keys.Count);

            foreach (var key in exception.Data.Keys)
            {
                if (key.Equals(MsStackTraceKey))
                {
                    continue;
                }

                var value = exception.Data[key];
                data.Add(key, value);
            }

            return data.Count > 0 ? data : null;
        }
    }
}

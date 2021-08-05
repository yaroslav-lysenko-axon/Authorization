using System;
using Authorization.Domain.Services.Abstraction;

namespace Authorization.Domain.Services
{
    public class TimeProvider : ITimeProvider
    {
        public DateTime UtcNow() => DateTime.UtcNow;
    }
}

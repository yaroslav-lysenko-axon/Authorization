using System;

namespace Authorization.Domain.Services.Abstraction
{
    public interface ITimeProvider
    {
        DateTime UtcNow();
    }
}

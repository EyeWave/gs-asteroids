using System;

namespace GS.Asteroids.Core.Interfaces
{
    public interface IDebugLogger : IDisposable
    {
        void Log(string message);

        void LogWarning(string message);

        void LogError(string message);

        void LogException(Exception exception);
    }
}

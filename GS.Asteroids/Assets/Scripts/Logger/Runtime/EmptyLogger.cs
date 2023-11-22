using GS.Asteroids.Core.Interfaces;
using System;

namespace GS.Asteroids.Logger
{
    internal sealed class EmptyLogger : IDebugLogger
    {
        public void Log(string message)
        {
        }

        public void LogWarning(string message)
        {
        }

        public void LogError(string message)
        {
        }

        public void LogException(Exception exception)
        {
        }

        public void Dispose()
        {
        }
    }
}

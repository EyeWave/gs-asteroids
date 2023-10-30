using GS.Asteroids.Core.Interfaces;
using System;
using UnityEngine;

namespace GS.Asteroids.Logger
{
    internal class UnityDebugLogger : IDebugLogger
    {
        public void Log(string message)
        {
            Debug.Log(message);
        }

        public void LogWarning(string message)
        {
            Debug.LogWarning(message);
        }

        public void LogError(string message)
        {
            Debug.LogError(message);
        }

        public void LogException(Exception exception)
        {
            Debug.LogException(exception);
        }

        public void Dispose()
        {
            Log("Finish logs");
        }
    }
}

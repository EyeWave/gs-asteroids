using GS.Asteroids.Core.Interfaces;

namespace GS.Asteroids.Logger
{
    public static class AsteroidsLoggerFactory
    {
        public static IDebugLogger Create()
        {
#if UNITY_EDITOR
            return new UnityDebugLogger();
#else
            return new EmptyLogger();
#endif
        }
    }
}

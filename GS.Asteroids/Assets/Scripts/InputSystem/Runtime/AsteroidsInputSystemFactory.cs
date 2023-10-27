using GS.Asteroids.Core.Interfaces;

namespace GS.Asteroids.InputSystem
{
    public static class AsteroidsInputSystemFactory
    {
        public static IInputSystem Create()
        {
            return new AsteroidsInputSystemProvider();
        }
    }
}

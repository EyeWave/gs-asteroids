using GS.Asteroids.Core.Interfaces;
using UnityEngine;

namespace GS.Asteroids.DrawSystem
{
    public static class AsteroidsDrawSystemFactory
    {
        public static IDrawSystem Create()
        {
            return new GameObject($"[{nameof(UnityLineRendererDrawSystem)}]")
                .AddComponent<UnityLineRendererDrawSystem>();
        }
    }
}

using System;
using Vector2 = UnityEngine.Vector2;

namespace GS.Asteroids.Core.Interfaces
{
    public interface IInputSystem : IDisposable
    {
        event Action Fire;

        event Action AlternativeFire;

        Vector2 GetMove();
    }
}

using System;
using UnityEngine;

namespace GS.Asteroids.Core.Interfaces
{
    public interface IInputSystem : IDisposable
    {
        event Action Fire;

        event Action AlternativeFire;

        Vector2 GetMove();
    }
}

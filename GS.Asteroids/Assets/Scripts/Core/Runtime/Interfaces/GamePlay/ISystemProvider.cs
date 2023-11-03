using System;

namespace GS.Asteroids.Core.Interfaces.GamePlay
{
    internal interface ISystemProvider : IDisposable
    {
        void Add(ISystem system);

        void Remove(ISystem system);
    }
}

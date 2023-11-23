using GS.Asteroids.Core.Entity;
using System;

namespace GS.Asteroids.Core.Interfaces.GamePlay
{
    internal interface IObjectProvider : IDisposable
    {
        T Take<T>() where T : class, IEntity;

        void Return(IEntity entity);
    }
}

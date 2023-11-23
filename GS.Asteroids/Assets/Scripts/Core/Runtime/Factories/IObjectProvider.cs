using GS.Asteroids.Core.Entity;
using System;

namespace GS.Asteroids.Core.Factories
{
    internal interface IObjectProvider<out T> where T : class, IEntity
    {
        Type EntityType => typeof(T);

        T Take();

        void Return(IEntity @object);
    }
}

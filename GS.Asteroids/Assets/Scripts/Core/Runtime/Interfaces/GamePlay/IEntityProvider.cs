using GS.Asteroids.Core.Entity;
using System;
using System.Collections.Generic;

namespace GS.Asteroids.Core.Interfaces.GamePlay
{
    internal interface IEntityProvider : ISystem, IDisposable
    {
        void Add(IEntity entity);

        void AddRange(IEnumerable<IEntity> entities);

        bool Remove(IEntity entity);
    }
}

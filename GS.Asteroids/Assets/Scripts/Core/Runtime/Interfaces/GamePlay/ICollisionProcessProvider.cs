using GS.Asteroids.Core.Objects;
using System.Collections.Generic;

namespace GS.Asteroids.Core.Interfaces.GamePlay
{
    internal interface ICollisionProcessProvider
    {
        IEnumerable<Collision> Collisions { get; }

        void Clear();
    }
}

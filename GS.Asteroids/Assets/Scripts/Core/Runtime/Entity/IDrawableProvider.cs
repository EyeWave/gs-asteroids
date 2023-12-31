using System.Collections.Generic;
using Vector3 = UnityEngine.Vector3;

namespace GS.Asteroids.Core.Entity
{
    internal interface IDrawableProvider : IDrawableEntity
    {
        Vector3 Position { get; }
        float Rotation { get; }
        IReadOnlyList<Vector3> CorePoints { get; }
    }
}

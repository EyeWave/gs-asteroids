using Vector3 = UnityEngine.Vector3;

namespace GS.Asteroids.Core.Entity
{
    internal interface ICollidable : IEntity
    {
        Vector3 Position { get; }
        float Radius { get; }
    }
}

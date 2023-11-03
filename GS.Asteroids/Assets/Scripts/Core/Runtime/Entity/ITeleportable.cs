using Vector3 = UnityEngine.Vector3;

namespace GS.Asteroids.Core.Entity
{
    internal interface ITeleportable : IEntity
    {
        Vector3 Position { get; set; }
        Vector3 Velocity { get; }
        float Radius { get; }
    }
}

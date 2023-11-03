using Vector3 = UnityEngine.Vector3;

namespace GS.Asteroids.Core.Entity
{
    internal interface IMovable : IEntity
    {
        Vector3 Position { get; set; }
        Vector3 Velocity { get; }
    }
}

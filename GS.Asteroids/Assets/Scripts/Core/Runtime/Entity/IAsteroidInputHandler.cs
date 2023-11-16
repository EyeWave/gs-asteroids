using Vector3 = UnityEngine.Vector3;

namespace GS.Asteroids.Core.Entity
{
    internal interface IAsteroidInputHandler : IEntity
    {
        Vector3 Velocity { get; set; }
        float Acceleration { get; }
    }
}

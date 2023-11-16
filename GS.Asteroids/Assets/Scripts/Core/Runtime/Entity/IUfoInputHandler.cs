using Vector3 = UnityEngine.Vector3;

namespace GS.Asteroids.Core.Entity
{
    internal interface IUfoInputHandler : IEntity
    {
        Vector3 Position { get; }
        Vector3 Velocity { get; set; }
        float Acceleration { get; }
    }
}

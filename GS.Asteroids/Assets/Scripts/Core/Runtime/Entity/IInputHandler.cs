using Vector3 = UnityEngine.Vector3;

namespace GS.Asteroids.Core.Entity
{
    internal interface IInputHandler : IEntity
    {
        float Rotation { get; }
        Vector3 Velocity { get; set; }
        float Acceleration { get; set; }
        float AngularAcceleration { get; set; }
    }
}

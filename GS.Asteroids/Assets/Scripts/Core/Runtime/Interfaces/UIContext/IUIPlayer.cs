using GS.Asteroids.Core.Entity;
using Vector3 = UnityEngine.Vector3;

namespace GS.Asteroids.Core.Interfaces.UIContext
{
    internal interface IUIPlayer : IEntity
    {
        Vector3 Position { get; }
        Vector3 Velocity { get; }
        float Rotation { get; }
    }
}

using Vector3 = UnityEngine.Vector3;

namespace GS.Asteroids.Core.Entity
{
    internal interface ILaserDrawableProvider : IDrawableEntity
    {
        Vector3 Position { get; }
    }
}

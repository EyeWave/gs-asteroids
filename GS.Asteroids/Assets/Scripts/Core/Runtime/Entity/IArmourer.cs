using Vector3 = UnityEngine.Vector3;

namespace GS.Asteroids.Core.Entity
{
    internal interface IArmourer : IEntity
    {
        Vector3 Position { get; }
        float Rotation { get; }
        Vector3 Direction { get; }
        float Radius { get; }
    }
}

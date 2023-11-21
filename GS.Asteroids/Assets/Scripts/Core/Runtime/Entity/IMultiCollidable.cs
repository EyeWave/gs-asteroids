using Vector3 = UnityEngine.Vector3;

namespace GS.Asteroids.Core.Entity
{
    internal interface IMultiCollidable : ICollidable
    {
        Vector3 Tail { get; }
    }
}

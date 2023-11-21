using GS.Asteroids.Core.Entity;
using Vector3 = UnityEngine.Vector3;

namespace GS.Asteroids.Core.Objects
{
    internal sealed class Laser : IMovable, IMultiCollidable, ILevelCollidable, ILaserDrawableProvider
    {
        public Vector3 Position { get; set; }
        public Vector3 Velocity { get; set; }

        public float Acceleration { get; set; }

        public Vector3 Tail { get; set; }

        public PointsContainer PointsContainer { get; set; }
        public float Radius => PointsContainer.Radius;

        public Vector3[] GetPoints() => PointsContainer.Points;
    }
}

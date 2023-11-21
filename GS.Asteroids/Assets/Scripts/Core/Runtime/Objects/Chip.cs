using GS.Asteroids.Core.Entity;
using System.Collections.Generic;
using Vector3 = UnityEngine.Vector3;

namespace GS.Asteroids.Core.Objects
{
    internal sealed class Chip : IMovable, ITeleportable, ICollidable, IDrawableProvider
    {
        public Vector3 Position { get; set; }
        public Vector3 Velocity { get; set; }
        public float Rotation => 0.0f;

        public PointsContainer PointsContainer { get; set; }
        public float Radius => PointsContainer.Radius;
        public IReadOnlyList<Vector3> CorePoints => PointsContainer.CorePoints;

        public Vector3[] GetPoints() => PointsContainer.Points;
    }
}
using System.Collections.Generic;
using Vector3 = UnityEngine.Vector3;

namespace GS.Asteroids.Core.Entity
{
    internal struct PointsContainer
    {
        public static readonly PointsContainer Default = new PointsContainer();

        public float Radius { get; }
        public Vector3[] Points { get; }
        public IReadOnlyList<Vector3> CorePoints { get; }

        public PointsContainer(float radius, IReadOnlyList<Vector3> corePoints)
        {
            Radius = radius;
            CorePoints = corePoints;

            Points = new Vector3[corePoints.Count];
            for (int i = 0; i < corePoints.Count; i++)
                Points[i] = CorePoints[i];
        }
    }
}

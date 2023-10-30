using GS.Asteroids.Core.Entity;
using GS.Asteroids.Core.Interfaces;
using System.Collections.Generic;
using Mathf = UnityEngine.Mathf;
using Vector3 = UnityEngine.Vector3;

namespace GS.Asteroids.Core.Systems
{
    internal sealed class RefreshDrawablePointsSystem : SystemCollectionProviderBase<IDrawableProvider>, IRefreshable
    {
        public RefreshDrawablePointsSystem(int capacity = 1024) : base(capacity)
        {
        }

        internal override void Add(IDrawableProvider @object)
        {
            base.Add(@object);

            RefreshDrawablePoints(@object);
        }

        public void Refresh()
        {
            foreach (IDrawableProvider drawableProvider in Collection)
                if (drawableProvider != null)
                    RefreshDrawablePoints(drawableProvider);
        }

        private void RefreshDrawablePoints(IDrawableProvider @object)
        {
            Vector3[] points = @object.GetPoints();
            IReadOnlyList<Vector3> corePoints = @object.GetCorePoints();

            for (int i = 0; i < corePoints.Count; i++)
            {
                Vector3 movePoint = corePoints[i] + @object.Position;

                float rotationRad = @object.Rotation * Mathf.Deg2Rad;
                float deltaX = movePoint.x - @object.Position.x;
                float deltaY = movePoint.y - @object.Position.y;
                float cosRotationRad = Mathf.Cos(rotationRad);
                float sinRotationRad = Mathf.Sin(rotationRad);

                movePoint.x = @object.Position.x + deltaX * cosRotationRad - deltaY * sinRotationRad;
                movePoint.y = @object.Position.y + deltaX * sinRotationRad + deltaY * cosRotationRad;

                points[i] = movePoint;
            }
        }
    }
}

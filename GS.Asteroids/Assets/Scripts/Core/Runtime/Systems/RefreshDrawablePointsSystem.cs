using GS.Asteroids.Core.Entity;
using GS.Asteroids.Core.Interfaces;
using System.Collections.Generic;
using Mathf = UnityEngine.Mathf;
using Vector3 = UnityEngine.Vector3;

namespace GS.Asteroids.Core.Systems
{
    internal sealed class RefreshDrawablePointsSystem : SystemCollectionProviderBase<IDrawableProvider>, IRefreshable
    {
        internal RefreshDrawablePointsSystem() : base()
        {
        }

        public override void Add(IEntity entity)
        {
            base.Add(entity);

            if (entity is IDrawableProvider entityOfCollection)
                RefreshDrawablePoints(entityOfCollection);
        }

        public void Refresh()
        {
            foreach (IDrawableProvider entityOfCollection in Collection)
                if (entityOfCollection != null)
                    RefreshDrawablePoints(entityOfCollection);
        }

        private void RefreshDrawablePoints(IDrawableProvider entity)
        {
            Vector3[] points = entity.GetPoints();
            IReadOnlyList<Vector3> corePoints = entity.CorePoints;

            for (int i = 0; i < corePoints.Count; i++)
            {
                Vector3 movePoint = corePoints[i] + entity.Position;

                float rotationRad = entity.Rotation * Mathf.Deg2Rad;
                float deltaX = movePoint.x - entity.Position.x;
                float deltaY = movePoint.y - entity.Position.y;
                float cosRotationRad = Mathf.Cos(rotationRad);
                float sinRotationRad = Mathf.Sin(rotationRad);

                movePoint.x = entity.Position.x + deltaX * cosRotationRad - deltaY * sinRotationRad;
                movePoint.y = entity.Position.y + deltaX * sinRotationRad + deltaY * cosRotationRad;

                points[i] = movePoint;
            }
        }
    }
}

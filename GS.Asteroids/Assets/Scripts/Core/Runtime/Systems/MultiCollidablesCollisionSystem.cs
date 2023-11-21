using GS.Asteroids.Core.Entity;
using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.Core.Interfaces.GamePlay;
using GS.Asteroids.Core.Objects;
using System;
using System.Collections.Generic;
using Vector3 = UnityEngine.Vector3;

namespace GS.Asteroids.Core.Systems
{
    internal sealed class MultiCollidablesCollisionSystem : DoubleSystemCollectionProviderBase<ICollidable, IMultiCollidable>, IRefreshable
    {
        private readonly ICollisionCreateProvider _collisionCreateProvider;
        private readonly HashSet<ICollidable> _markedEntities;

        internal MultiCollidablesCollisionSystem(ICollisionCreateProvider collisionCreateProvider) : base()
        {
            _collisionCreateProvider = collisionCreateProvider ?? throw new ArgumentNullException(nameof(collisionCreateProvider));
            _markedEntities = new HashSet<ICollidable>(1024);
        }

        public void Refresh()
        {
            foreach (IMultiCollidable multiCollidable in SecondCollection)
            {
                _markedEntities.Add(multiCollidable);

                foreach (ICollidable collidable in Collection)
                {
                    if (_markedEntities.Contains(collidable))
                        continue;

                    if (CheckPossibleCollision(multiCollidable, collidable) && CheckDistance(multiCollidable, collidable))
                        CreateCollision(collidable);
                }
            }

            _markedEntities.Clear();
        }

        private bool CheckPossibleCollision(IMultiCollidable multiCollidable, ICollidable collidable)
        {
            return multiCollidable is Laser && collidable is Asteroid or Chip or Ufo;
        }

        private bool CheckDistance(IMultiCollidable multiCollidable, ICollidable collidable)
        {
            Vector3 line = multiCollidable.Tail - multiCollidable.Position;
            Vector3 fromEndToPoint = collidable.Position - multiCollidable.Tail;
            Vector3 fromStartToPoint = collidable.Position - multiCollidable.Position;
            float sumRadius = multiCollidable.Radius + collidable.Radius;

            if (Vector3.Dot(line, fromEndToPoint) > 0.0f)
            {
                float deltaX = collidable.Position.x - multiCollidable.Tail.x;
                float deltaY = collidable.Position.y - multiCollidable.Tail.y;

                return deltaX * deltaX + deltaY * deltaY < sumRadius * sumRadius;
            }
            else if (Vector3.Dot(line, fromStartToPoint) < 0.0f)
            {
                float deltaX = collidable.Position.x - multiCollidable.Position.x;
                float deltaY = collidable.Position.y - multiCollidable.Position.y;

                return deltaX * deltaX + deltaY * deltaY < sumRadius * sumRadius;
            }
            else
            {
                //return (UnityEngine.Mathf.Abs(line.x * fromStartToPoint.y - line.y * fromStartToPoint.x) / line.magnitude) < sumRadius;

                float delta = line.x * fromStartToPoint.y - line.y * fromStartToPoint.x;

                return (delta * delta / (line.x * line.x + line.y * line.y)) < sumRadius * sumRadius;
            }
        }

        private void CreateCollision(ICollidable collidable)
        {
            if (collidable is IMultiCollidable)
                return;

            _collisionCreateProvider.Create(collidable);
        }
    }
}

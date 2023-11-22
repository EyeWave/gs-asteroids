using GS.Asteroids.Core.Entity;
using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.Core.Interfaces.GamePlay;
using GS.Asteroids.Core.Objects;
using System;
using System.Collections.Generic;

namespace GS.Asteroids.Core.Systems
{
    internal sealed class SimpleCollidablesCollisionSystem : SystemCollectionProviderBase<ICollidable>, IRefreshable
    {
        private readonly ICollisionCreateProvider _collisionCreateProvider;
        private readonly HashSet<ICollidable> _markedCollidable;

        internal SimpleCollidablesCollisionSystem(ICollisionCreateProvider collisionCreateProvider) : base()
        {
            _collisionCreateProvider = collisionCreateProvider ?? throw new ArgumentNullException(nameof(collisionCreateProvider));
            _markedCollidable = new HashSet<ICollidable>(1024);
        }

        public override void Init()
        {
            base.Init();

            _markedCollidable.Clear();
        }

        public void Refresh()
        {
            foreach (ICollidable firstCollidable in Collection)
            {
                _markedCollidable.Add(firstCollidable);

                foreach (ICollidable secondCollidable in Collection)
                {
                    if (_markedCollidable.Contains(secondCollidable))
                        continue;

                    if (CheckPossibleCollision(firstCollidable, secondCollidable) && CheckDistance(firstCollidable, secondCollidable))
                        _collisionCreateProvider.Create(firstCollidable, secondCollidable);
                }
            }

            _markedCollidable.Clear();
        }

        private bool CheckPossibleCollision(ICollidable firstCollidable, ICollidable secondCollidable)
        {
            return CheckCollision(firstCollidable, secondCollidable) || CheckCollision(secondCollidable, firstCollidable);
        }

        private bool CheckCollision(ICollidable firstCollidable, ICollidable secondCollidable)
        {
            return firstCollidable is Bullet or PlayerShip && secondCollidable is Asteroid or Chip or Ufo;
        }

        private bool CheckDistance(ICollidable firstCollidable, ICollidable secondCollidable)
        {
            float deltaX = firstCollidable.Position.x - secondCollidable.Position.x;
            float deltaY = firstCollidable.Position.y - secondCollidable.Position.y;
            float sumRadius = firstCollidable.Radius + secondCollidable.Radius;
            return deltaX * deltaX + deltaY * deltaY < sumRadius * sumRadius;
        }

        private void CreateCollision(ICollidable firstCollidable, ICollidable secondCollidable)
        {
            _collisionCreateProvider.Create
            (
                firstCollidable is IMultiCollidable ? null : firstCollidable,
                secondCollidable is IMultiCollidable ? null : secondCollidable
            );
        }
    }
}

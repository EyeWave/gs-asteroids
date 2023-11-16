using GS.Asteroids.Core.Entity;
using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.Core.Interfaces.GamePlay;
using GS.Asteroids.Core.Objects;
using System;
using System.Collections.Generic;

namespace GS.Asteroids.Core.Systems
{
    internal sealed class CollidablesCollisionSystem : SystemCollectionProviderBase<ICollidable>, IRefreshable
    {
        private readonly ICollisionCreateProvider _collisionCreateProvider;
        private readonly HashSet<ICollidable> _markedEntities;

        internal CollidablesCollisionSystem(ICollisionCreateProvider collisionCreateProvider) : base()
        {
            _collisionCreateProvider = collisionCreateProvider ?? throw new ArgumentNullException(nameof(collisionCreateProvider));
            _markedEntities = new HashSet<ICollidable>(1024);
        }

        public void Refresh()
        {
            foreach (ICollidable firstEntity in Collection)
            {
                _markedEntities.Add(firstEntity);

                foreach (ICollidable secondEntity in Collection)
                {
                    if (_markedEntities.Contains(secondEntity))
                        continue;

                    if (CheckPossibleCollision(firstEntity, secondEntity) && CheckDistance(firstEntity, secondEntity))
                        CreateCollision(firstEntity, secondEntity);
                }
            }

            _markedEntities.Clear();
        }

        private bool CheckPossibleCollision(ICollidable firstEntity, ICollidable secondEntity)
        {
            bool result = false;

            if (firstEntity is Bullet)
                result = CheckPossibleCollisionWithBullet(secondEntity);
            else if (secondEntity is Bullet)
                result = CheckPossibleCollisionWithBullet(firstEntity);
            else if (firstEntity is PlayerShip)
                result = CheckPossibleCollisionWithPlayerShip(secondEntity);
            else if (secondEntity is PlayerShip)
                result = CheckPossibleCollisionWithPlayerShip(firstEntity);

            return result;
        }

        private bool CheckPossibleCollisionWithBullet(ICollidable entity)
        {
            return entity is Asteroid || entity is Ufo;
        }

        private bool CheckPossibleCollisionWithPlayerShip(ICollidable entity)
        {
            return entity is Asteroid || entity is Ufo;
        }

        private bool CheckDistance(ICollidable firstEntity, ICollidable secondEntity)
        {
            float deltaX = firstEntity.Position.x - secondEntity.Position.x;
            float deltaY = firstEntity.Position.y - secondEntity.Position.y;
            float sumRadius = firstEntity.Radius + secondEntity.Radius;
            return deltaX * deltaX + deltaY * deltaY < sumRadius * sumRadius;
        }

        private void CreateCollision(ICollidable firstEntity, ICollidable secondEntity)
        {
            _collisionCreateProvider.Create(firstEntity, secondEntity);
        }
    }
}

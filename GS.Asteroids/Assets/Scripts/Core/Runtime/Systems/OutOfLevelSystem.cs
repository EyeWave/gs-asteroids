using GS.Asteroids.Core.Entity;
using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.Core.Interfaces.GamePlay;
using System;

namespace GS.Asteroids.Core.Systems
{
    internal sealed class OutOfLevelSystem : SystemCollectionProviderBase<ILevelCollidable>, IRefreshable
    {
        private readonly ILevel _level;
        private readonly ICollisionCreateProvider _collisionCreateProvider;

        internal OutOfLevelSystem(ILevel level, ICollisionCreateProvider collisionCreateProvider) : base()
        {
            _level = level ?? throw new ArgumentNullException(nameof(level));
            _collisionCreateProvider = collisionCreateProvider ?? throw new ArgumentNullException(nameof(collisionCreateProvider));
        }

        public void Refresh()
        {
            foreach (ILevelCollidable entityOfCollection in Collection)
                if (entityOfCollection != null)
                    TryCreateCollision(entityOfCollection);
        }

        private void TryCreateCollision(ILevelCollidable entity)
        {
            if (entity.Position.y > _level.Top + entity.Radius)
                CreateCollision(entity);
            else if (entity.Position.y < _level.Bottom - entity.Radius)
                CreateCollision(entity);
            else if (entity.Position.x < _level.Left - entity.Radius)
                CreateCollision(entity);
            else if (entity.Position.x > _level.Right + entity.Radius)
                CreateCollision(entity);
        }

        private void CreateCollision(ILevelCollidable firstEntity)
        {
            _collisionCreateProvider.Create(firstEntity);
        }
    }
}

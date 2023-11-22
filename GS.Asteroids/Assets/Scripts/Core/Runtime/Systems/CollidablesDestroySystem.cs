using GS.Asteroids.Core.Entity;
using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.Core.Interfaces.GamePlay;
using GS.Asteroids.Core.Objects;
using System;

namespace GS.Asteroids.Core.Systems
{
    internal sealed class CollidablesDestroySystem : ISystem, IRefreshable
    {
        private readonly ICollisionProcessProvider _collisionProcessProvider;
        private readonly IEntityProvider _entityProvider;
        private readonly IObjectProvider _objectProvider;

        internal CollidablesDestroySystem(
            ICollisionProcessProvider collisionProcessProvider,
            IEntityProvider entityProvider,
            IObjectProvider objectProvider)
        {
            _collisionProcessProvider = collisionProcessProvider ?? throw new ArgumentNullException(nameof(collisionProcessProvider));
            _entityProvider = entityProvider ?? throw new ArgumentNullException(nameof(entityProvider));
            _objectProvider = objectProvider ?? throw new ArgumentNullException(nameof(objectProvider));
        }

        public void Refresh()
        {
            foreach (Collision collision in _collisionProcessProvider.Collisions)
                CollidablesDestroy(collision);
        }

        private void CollidablesDestroy(Collision collision)
        {
            if (collision.First is IMultiCollidable firstMultiCollidable)
            {
                Destroy(firstMultiCollidable, collision.Second);
            }
            else if (collision.Second is IMultiCollidable secondMultiCollidable)
            {
                Destroy(secondMultiCollidable, collision.First);
            }
            else
            {
                Destroy(collision.First);
                Destroy(collision.Second);
            }
        }

        private void Destroy(IMultiCollidable multiCollidable, ICollidable collidable)
        {
            if (collidable == null)
                Destroy(multiCollidable);
            else
                Destroy(collidable);
        }

        private void Destroy(ICollidable collidable)
        {
            if (collidable == null)
                return;

            _entityProvider.Remove(collidable);
            _objectProvider.Return(collidable);
        }
    }
}

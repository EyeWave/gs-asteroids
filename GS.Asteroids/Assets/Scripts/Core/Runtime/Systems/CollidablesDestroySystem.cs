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

            _collisionProcessProvider.Clear();
        }

        private void CollidablesDestroy(Collision collision)
        {
            if (collision.First != null)
            {
                _entityProvider.Remove(collision.First);
                _objectProvider.Return(collision.First);
            }

            if (collision.Second != null)
            {
                _entityProvider.Remove(collision.Second);
                _objectProvider.Return(collision.Second);
            }
        }
    }
}

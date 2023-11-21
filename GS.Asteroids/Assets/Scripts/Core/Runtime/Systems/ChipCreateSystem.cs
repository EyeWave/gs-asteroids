using GS.Asteroids.Core.Entity;
using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.Core.Interfaces.Configuration;
using GS.Asteroids.Core.Interfaces.GamePlay;
using GS.Asteroids.Core.Objects;
using System;
using Mathf = UnityEngine.Mathf;
using Vector3 = UnityEngine.Vector3;

namespace GS.Asteroids.Core.Systems
{
    internal sealed class ChipCreateSystem : ISystem, IRefreshable
    {
        private readonly int _quantityOnDestroyOfAsteroid;
        private readonly ICollisionProcessProvider _collisionProcessProvider;
        private readonly IEntityProvider _entityProvider;
        private readonly IObjectProvider _objectProvider;

        internal ChipCreateSystem(
            IAppConfigDataProvider appConfigDataProvider,
            ICollisionProcessProvider collisionProcessProvider,
            IEntityProvider entityProvider,
            IObjectProvider objectProvider) : base()
        {
            IChipConfig config = appConfigDataProvider?.GetConfig<IChipConfig>() ?? throw new ArgumentNullException(nameof(IChipConfig));
            _quantityOnDestroyOfAsteroid = Mathf.Max(0, config.QuantityOnDestroyOfAsteroid);

            _collisionProcessProvider = collisionProcessProvider ?? throw new ArgumentNullException(nameof(collisionProcessProvider));
            _entityProvider = entityProvider ?? throw new ArgumentNullException(nameof(entityProvider));
            _objectProvider = objectProvider ?? throw new ArgumentNullException(nameof(objectProvider));
        }

        public void Refresh()
        {
            foreach (Collision collision in _collisionProcessProvider.Collisions)
                ChipCreate(collision);
        }

        private void ChipCreate(Collision collision)
        {
            if (collision.First != null)
                Create(collision.First);

            if (collision.Second != null)
                Create(collision.Second);
        }

        private void Create(ICollidable collidable)
        {
            if (collidable is Asteroid)
                for (int i = 0; i < _quantityOnDestroyOfAsteroid; i++)
                    _entityProvider.Add(Create(collidable.Position, collidable.Radius));
        }

        private IEntity Create(Vector3 position, float offset)
        {
            Chip @object = _objectProvider.Take<Chip>();

            @object.Position = position + @object.Velocity.normalized * offset;

            return @object;
        }
    }
}

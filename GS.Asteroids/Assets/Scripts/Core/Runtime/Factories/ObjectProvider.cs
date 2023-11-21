using GS.Asteroids.Core.Entity;
using GS.Asteroids.Core.Interfaces.GamePlay;
using GS.Asteroids.Core.Objects;
using System;
using System.Collections.Generic;

namespace GS.Asteroids.Core.Factories
{
    internal class ObjectProvider : IObjectProvider
    {
        private readonly ObjectProviderBase<PlayerShip> _playerShipProvider;
        private readonly ObjectProviderBase<Asteroid> _asteroidProvider;
        private readonly ObjectProviderBase<Chip> _chipProvider;
        private readonly ObjectProviderBase<Ufo> _ufoProvider;
        private readonly ObjectProviderBase<Bullet> _bulletProvider;
        private readonly ObjectProviderBase<Laser> _laserProvider;

        private IList<IEntity> _activeObjects;

        public ObjectProvider(
            ObjectProviderBase<PlayerShip> playerShipProvider,
            ObjectProviderBase<Asteroid> asteroidProvider,
            ObjectProviderBase<Chip> chipProvider,
            ObjectProviderBase<Ufo> ufoProvider,
            ObjectProviderBase<Bullet> bulletProvider,
            ObjectProviderBase<Laser> laserProvider)
        {
            _playerShipProvider = playerShipProvider ?? throw new ArgumentNullException(nameof(playerShipProvider));
            _asteroidProvider = asteroidProvider ?? throw new ArgumentNullException(nameof(asteroidProvider));
            _chipProvider = chipProvider ?? throw new ArgumentNullException(nameof(chipProvider));
            _ufoProvider = ufoProvider ?? throw new ArgumentNullException(nameof(ufoProvider));
            _bulletProvider = bulletProvider ?? throw new ArgumentNullException(nameof(bulletProvider));
            _laserProvider = laserProvider ?? throw new ArgumentNullException(nameof(laserProvider));

            _activeObjects = new List<IEntity>(512);
        }

        public T Take<T>() where T : class, IEntity, new()
        {
            IEntity result;
            Type type = typeof(T);

            if (type == typeof(PlayerShip))
                result = _playerShipProvider.Take();
            else if (type == typeof(Asteroid))
                result = _asteroidProvider.Take();
            else if (type == typeof(Chip))
                result = _chipProvider.Take();
            else if (type == typeof(Ufo))
                result = _ufoProvider.Take();
            else if (type == typeof(Bullet))
                result = _bulletProvider.Take();
            else if (type == typeof(Laser))
                result = _laserProvider.Take();
            else
                throw new NotImplementedException($"Unknown type {type.Name}");

            _activeObjects.Add(result);

            return result as T;
        }

        public void Return(IEntity entity)
        {
            Type type = entity.GetType();

            if (type == typeof(PlayerShip))
                _playerShipProvider.Return(entity as PlayerShip);
            else if (type == typeof(Asteroid))
                _asteroidProvider.Return(entity as Asteroid);
            else if (type == typeof(Chip))
                _chipProvider.Return(entity as Chip);
            else if (type == typeof(Ufo))
                _ufoProvider.Return(entity as Ufo);
            else if (type == typeof(Bullet))
                _bulletProvider.Return(entity as Bullet);
            else if (type == typeof(Laser))
                _laserProvider.Return(entity as Laser);
            else
                throw new NotImplementedException($"Unknown type {type.Name}");

            _activeObjects.Remove(entity);
        }

        public void Dispose()
        {
            while (_activeObjects.Count > 0)
                Return(_activeObjects[0]);
        }
    }
}

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
        private readonly ObjectProviderBase<Ufo> _ufoProvider;
        private readonly ObjectProviderBase<Bullet> _bulletProvider;

        private IList<IEntity> _activeObjects;

        public ObjectProvider(
            ObjectProviderBase<PlayerShip> playerShipProvider,
            ObjectProviderBase<Asteroid> asteroidProvider,
            ObjectProviderBase<Ufo> ufoProvider,
            ObjectProviderBase<Bullet> bulletProvider)
        {
            _playerShipProvider = playerShipProvider ?? throw new ArgumentNullException(nameof(playerShipProvider));
            _asteroidProvider = asteroidProvider ?? throw new ArgumentNullException(nameof(asteroidProvider));
            _ufoProvider = ufoProvider ?? throw new ArgumentNullException(nameof(ufoProvider));
            _bulletProvider = bulletProvider ?? throw new ArgumentNullException(nameof(bulletProvider));

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
            else if (type == typeof(Ufo))
                result = _ufoProvider.Take();
            else if (type == typeof(Bullet))
                result = _bulletProvider.Take();
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
            else if (type == typeof(Ufo))
                _ufoProvider.Return(entity as Ufo);
            else if (type == typeof(Bullet))
                _bulletProvider.Return(entity as Bullet);
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

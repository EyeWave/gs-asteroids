using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.Core.Interfaces.GamePlay;
using GS.Asteroids.Core.Objects;

namespace GS.Asteroids.Core.Factories
{
    internal static class ObjectProviderFactory
    {
        internal static IObjectProvider Create(ILevel level, IAppConfigDataProvider appConfigDataProvider)
        {
            ObjectFactory<PlayerShip> playerShipFactory = new ObjectFactory<PlayerShip>();
            ObjectProviderBase<PlayerShip> playerShipProvider = new PlayerShipObjectProvider(playerShipFactory, level.GetPlayerStartPosition, appConfigDataProvider);

            ObjectFactory<Asteroid> asteroidFactory = new ObjectFactory<Asteroid>();
            ObjectProviderBase<Asteroid> asteroidProvider = new AsteroidObjectProvider(asteroidFactory, level.GetEnemyStartPosition, appConfigDataProvider);

            ObjectFactory<Ufo> ufoFactory = new ObjectFactory<Ufo>();
            ObjectProviderBase<Ufo> ufoProvider = new UfoObjectProvider(ufoFactory, level.GetEnemyStartPosition, appConfigDataProvider);

            ObjectFactory<Bullet> bulletFactory = new ObjectFactory<Bullet>();
            ObjectProviderBase<Bullet> bulletProvider = new BulletObjectProvider(bulletFactory, appConfigDataProvider);

            return new ObjectProvider(
                playerShipProvider,
                asteroidProvider,
                ufoProvider,
                bulletProvider
            );
        }
    }
}

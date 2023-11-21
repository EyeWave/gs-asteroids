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
            ObjectProviderBase<PlayerShip> playerShipProvider = new PlayerShipProvider(playerShipFactory, level.GetPlayerStartPosition, appConfigDataProvider);

            ObjectFactory<Asteroid> asteroidFactory = new ObjectFactory<Asteroid>();
            ObjectProviderBase<Asteroid> asteroidProvider = new AsteroidProvider(asteroidFactory, level.GetEnemyStartPosition, appConfigDataProvider);

            ObjectFactory<Chip> chipFactory = new ObjectFactory<Chip>();
            ObjectProviderBase<Chip> chipProvider = new ChipProvider(chipFactory, appConfigDataProvider);

            ObjectFactory<Ufo> ufoFactory = new ObjectFactory<Ufo>();
            ObjectProviderBase<Ufo> ufoProvider = new UfoProvider(ufoFactory, level.GetEnemyStartPosition, appConfigDataProvider);

            ObjectFactory<Bullet> bulletFactory = new ObjectFactory<Bullet>();
            ObjectProviderBase<Bullet> bulletProvider = new BulletProvider(bulletFactory, appConfigDataProvider);
            
            ObjectFactory<Laser> laserFactory = new ObjectFactory<Laser>();
            ObjectProviderBase<Laser> laserProvider = new LaserProvider(laserFactory, appConfigDataProvider);

            return new ObjectProvider(
                playerShipProvider,
                asteroidProvider,
                chipProvider,
                ufoProvider,
                bulletProvider,
                laserProvider
            );
        }
    }
}

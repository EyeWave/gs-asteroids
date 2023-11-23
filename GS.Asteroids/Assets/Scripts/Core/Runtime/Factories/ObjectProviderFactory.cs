using GS.Asteroids.Core.Entity;
using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.Core.Interfaces.GamePlay;
using GS.Asteroids.Core.Objects;

namespace GS.Asteroids.Core.Factories
{
    internal static class ObjectProviderFactory
    {
        internal static IObjectProvider Create(ILevel level, IAppConfigDataProvider appConfigDataProvider)
        {
            ObjectFactoryBase<PlayerShip> playerShipFactory = new PlayerShipFactory();
            IObjectProvider<IEntity> playerShipProvider = new PlayerShipProvider(playerShipFactory, level.GetPlayerStartPosition, appConfigDataProvider);

            ObjectFactoryBase<Asteroid> asteroidFactory = new AsteroidFactory();
            IObjectProvider<IEntity> asteroidProvider = new AsteroidProvider(asteroidFactory, level.GetEnemyStartPosition, appConfigDataProvider);

            ObjectFactoryBase<Chip> chipFactory = new ChipFactory();
            IObjectProvider<IEntity> chipProvider = new ChipProvider(chipFactory, appConfigDataProvider);

            ObjectFactoryBase<Ufo> ufoFactory = new UfoFactory();
            IObjectProvider<IEntity> ufoProvider = new UfoProvider(ufoFactory, level.GetEnemyStartPosition, appConfigDataProvider);

            ObjectFactoryBase<Bullet> bulletFactory = new BulletFactory();
            IObjectProvider<IEntity> bulletProvider = new BulletProvider(bulletFactory, appConfigDataProvider);

            ObjectFactoryBase<Laser> laserFactory = new LaserFactory();
            IObjectProvider<IEntity> laserProvider = new LaserProvider(laserFactory, appConfigDataProvider);

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

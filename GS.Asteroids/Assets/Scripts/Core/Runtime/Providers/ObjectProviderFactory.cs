using GS.Asteroids.Core.Entity;
using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.Core.Interfaces.GamePlay;
using GS.Asteroids.Core.Objects;

namespace GS.Asteroids.Core.Providers
{
    internal static class ObjectProviderFactory
    {
        internal static IObjectProvider Create(ILevel level, IAppConfigDataProvider appConfigDataProvider)
        {
            IObjectProvider<IEntity> playerShipProvider = new PlayerShipProvider
            (
                objectGenerator: () => new PlayerShip(),
                startPositionGenerator: level.GetPlayerStartPosition,
                appConfigDataProvider
            );

            IObjectProvider<IEntity> asteroidProvider = new AsteroidProvider
            (
                objectGenerator: () => new Asteroid(),
                startPositionGenerator: level.GetEnemyStartPosition,
                appConfigDataProvider
            );

            IObjectProvider<IEntity> chipProvider = new ChipProvider
            (
                objectGenerator: () => new Chip(),
                appConfigDataProvider
            );

            IObjectProvider<IEntity> ufoProvider = new UfoProvider
            (
                objectGenerator: () => new Ufo(),
                startPositionGenerator: level.GetEnemyStartPosition,
                appConfigDataProvider
            );

            IObjectProvider<IEntity> bulletProvider = new BulletProvider
            (
                objectGenerator: () => new Bullet(),
                appConfigDataProvider
            );

            IObjectProvider<IEntity> laserProvider = new LaserProvider
            (
                objectGenerator: () => new Laser(),
                appConfigDataProvider
            );

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

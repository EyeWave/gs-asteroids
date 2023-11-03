using GS.Asteroids.Core.Interfaces;
using GS.Asteroids.Core.Interfaces.AppStates;
using GS.Asteroids.Core.Interfaces.GamePlay;
using GS.Asteroids.Core.Objects;
using GS.Asteroids.Core.Systems;
using System.Collections.Generic;

namespace GS.Asteroids.Core.States
{
    internal sealed class GamePlaySate : IAppState
    {
        public AppState State => AppState.GamePlay;

        private readonly IEntityProvider _entityProvider;
        private readonly IObjectProvider _objectProvider;
        private readonly ISystemProvider _systemProvider;

        private readonly IReadOnlyCollection<ISystem> _systems;

        internal GamePlaySate(
            CompositeProvider compositeProvider,
            ILevel level,
            IAppConfigDataProvider appConfigDataProvider,
            IInputSystem inputSystem,
            IDrawSystem drawSystem)
        {
            _entityProvider = compositeProvider;
            _objectProvider = compositeProvider;
            _systemProvider = compositeProvider;

            ICollisionCreateProvider collisionCreateProvider = compositeProvider;
            ICollisionProcessProvider collisionProcessProvider = compositeProvider;

            PlayerShipInputSystem playerShipInputSystem = new PlayerShipInputSystem(appConfigDataProvider, inputSystem);
            RotateSystem rotateSystem = new RotateSystem();
            MoveSystem moveSystem = new MoveSystem();
            TeleportSystem teleportSystem = new TeleportSystem(level);

            AsteroidCreateSystem asteroidCreateSystem = new AsteroidCreateSystem(appConfigDataProvider, _entityProvider, _objectProvider);
            BulletCreateSystem bulletCreateSystem = new BulletCreateSystem(inputSystem, _entityProvider, _objectProvider);
            OutOfLeveSystem outOfLeveSystem = new OutOfLeveSystem(level, collisionCreateProvider);
            CollidablesCollisionSystem collidablesCollisionSystem = new CollidablesCollisionSystem(collisionCreateProvider);

            CollidablesDestroySystem collidablesDestroySystem = new CollidablesDestroySystem(collisionProcessProvider, _entityProvider, _objectProvider);
            RefreshDrawablePointsSystem refreshDrawablePointsSystem = new RefreshDrawablePointsSystem();
            DrawSystemProvider drawSystemProvider = new DrawSystemProvider(drawSystem);

            _systems = new List<ISystem>
            {
                playerShipInputSystem,
                rotateSystem,
                moveSystem,
                teleportSystem,
                asteroidCreateSystem,
                bulletCreateSystem,
                outOfLeveSystem,
                collidablesCollisionSystem,
                collidablesDestroySystem,
                refreshDrawablePointsSystem,
                drawSystemProvider,
            };
        }

        public void Enter()
        {
            foreach (ISystem system in _systems)
            {
                _systemProvider.Add(system);
                system.Init();
            }

            _entityProvider.Add(_objectProvider.Take<PlayerShip>());
        }

        public void Exit()
        {
            _entityProvider.Dispose();
            _objectProvider.Dispose();

            foreach (ISystem system in _systems)
                _systemProvider.Remove(system);
        }

        public bool MoveNext()
        {
            return false;
        }
    }
}

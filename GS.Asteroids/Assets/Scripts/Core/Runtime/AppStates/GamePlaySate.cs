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

        private bool _isGameOver = false;

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

            _systems = new List<ISystem>
            {
                new PlayerShipInputSystem(appConfigDataProvider, inputSystem),
                new AsteroidCreateSystem(appConfigDataProvider, _entityProvider, _objectProvider),
                new AsteroidInputSystem(),
                new UfoCreateSystem(appConfigDataProvider, _entityProvider, _objectProvider),
                new UfoInputSystem(),
                new BulletCreateSystem(inputSystem, _entityProvider, _objectProvider),

                new RotateSystem(),
                new MoveSystem(),
                new TeleportSystem(level),

                new OutOfLeveSystem(level, collisionCreateProvider),
                new CollidablesCollisionSystem(collisionCreateProvider),

                new CollidablesDestroySystem(collisionProcessProvider, _entityProvider, _objectProvider),
                new RefreshDrawablePointsSystem(),
                new DrawSystemProvider(drawSystem),

                new GameOverSystem(() => _isGameOver = true),
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

            _isGameOver = false;
        }

        public bool MoveNext()
        {
            return _isGameOver;
        }
    }
}

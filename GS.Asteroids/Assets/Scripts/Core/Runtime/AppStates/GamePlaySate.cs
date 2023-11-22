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
            IDrawSystem drawSystem,
            IUiSystem uiSystem,
            IDebugLogger logger)
        {
            _entityProvider = compositeProvider;
            _objectProvider = compositeProvider;
            _systemProvider = compositeProvider;

            ICollisionCreateProvider collisionCreateProvider = compositeProvider;
            ICollisionProcessProvider collisionProcessProvider = compositeProvider;
            IResultProvider resultProvider = compositeProvider;
            InputSystemDecorator inputSystemDecorator = new InputSystemDecorator(appConfigDataProvider, inputSystem, logger);

            _systems = new List<ISystem>
            {
                inputSystemDecorator,
                new PlayerShipInputSystem(appConfigDataProvider, inputSystemDecorator),
                new AsteroidCreateSystem(appConfigDataProvider, _entityProvider, _objectProvider),
                new AsteroidInputSystem(),
                new UfoCreateSystem(appConfigDataProvider, _entityProvider, _objectProvider),
                new UfoInputSystem(appConfigDataProvider),
                new BulletCreateSystem(inputSystemDecorator, _entityProvider, _objectProvider),
                new LaserCreateSystem(level, inputSystemDecorator, _entityProvider, _objectProvider),

                new RotateSystem(),
                new MoveSystem(),
                new TeleportSystem(level),

                new OutOfLevelSystem(level, collisionCreateProvider),
                new SimpleCollidablesCollisionSystem(collisionCreateProvider),
                new MultiCollidablesCollisionSystem(collisionCreateProvider),

                new ChipCreateSystem(appConfigDataProvider, collisionProcessProvider, _entityProvider, _objectProvider),
                new ResultCalculationSystem(appConfigDataProvider,collisionProcessProvider, resultProvider),
                new CollidablesDestroySystem(collisionProcessProvider, _entityProvider, _objectProvider),
                new RefreshClearSystem(collisionProcessProvider),

                new RefreshLaserDrawablePointsSystem(),
                new RefreshDrawablePointsSystem(),
                new DrawSystemProvider(drawSystem),
                new GamePlayUiRefreshSystem(uiSystem, inputSystemDecorator, logger),

                new GameOverSystem(() => _isGameOver = true),
            };
        }

        public void Enter()
        {
            _entityProvider.Add(_objectProvider.Take<PlayerShip>());

            foreach (ISystem system in _systems)
            {
                _systemProvider.Add(system);
                system.Init();
            }
        }

        public void Exit()
        {
            foreach (ISystem system in _systems)
                _systemProvider.Remove(system);

            _entityProvider.Dispose();
            _objectProvider.Dispose();
            _systemProvider.Dispose();

            _isGameOver = false;
        }

        public bool MoveNext()
        {
            return _isGameOver;
        }
    }
}
